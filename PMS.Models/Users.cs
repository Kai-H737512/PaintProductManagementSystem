using System;
using Microsoft.AspNetCore.Identity;

namespace PMS.Models;

public class Users : IdentityUser
{
    public string Account { get; set; }
}
