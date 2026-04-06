using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PMS.API.DTOs;
using PMS.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;

        public AuthController(IConfiguration configuration, UserManager<Users> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SigninRequestDto signinRequestDto)
        {
            var user = await _userManager.FindByNameAsync(signinRequestDto.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, signinRequestDto.Password);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = JWTGenerator(user);
            return Ok(token);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupRequestDto signupRequestDto)
        {
            // exist check
            var existingUser = await _userManager.FindByNameAsync(signupRequestDto.UserName);
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }

            // create user
            var newUser = new Users
            {
                UserName = signupRequestDto.UserName,
                Email = signupRequestDto.Email,
                Account = signupRequestDto.Account,
            };
            
            var createUserResult = await _userManager.CreateAsync(newUser, signupRequestDto.Password);
            if (!createUserResult.Succeeded)
            {
                return BadRequest("Failed to create user.");
            }
            
            var userRole = await _userManager.AddToRoleAsync(newUser, "User");
            if (!userRole.Succeeded)
            {
                return BadRequest("Failed to assign user role.");
            }

            // transactional operation: create user and assign role should be atomic, if any of them fails, the whole operation should be rolled back
            // but since we are using Identity framework, it will handle the transaction for us, if create user fails, it will not assign role, if assign role fails, it will not create user
            var token = JWTGenerator(newUser);
            return Ok(token);
        }

        private string JWTGenerator(Users user)
        {
            // JWT claim
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // add user role claims
            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach ( var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // generate JWT token: header, payload(claims), signature
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // encript with key and algorithm
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // construct the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signature
            );

            // serilize the token to string
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
