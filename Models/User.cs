using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TECsite.Data;
using TECsite.Models;

namespace TECsite.Models
{
    [PrimaryKey("UserName")]
    public class User
    {
        public User(string userName, string discordUser, string email, string password, bool emailConfirmed = false, string userRole = "User") 
        {
            UserName = userName;
            DiscordUser = discordUser;
            Email = email;
            Password = password;
            EmailConfirmed = emailConfirmed;
            UserRole = userRole;
        }

        public static string UserName { get; set; }
        public static string DiscordUser { get; set; }
        public static string Email { get; set; }
        public static bool EmailConfirmed { get; set; }
        public static string Password { get; set; }
        public static string UserRole { get; set; }

    }
}
