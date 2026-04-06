using System;

namespace PMS.API.DTOs;

public class SignupRequestDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Account { get; set; }
}
