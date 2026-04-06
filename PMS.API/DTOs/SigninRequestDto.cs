using System;

namespace PMS.API.DTOs;

public class SigninRequestDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
