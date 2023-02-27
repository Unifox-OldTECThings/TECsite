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
    public class User
    {
        public User(string UName, string DisUser, string email, bool EmailConf, string Pass, string URole) {
            UserName = UName;
            DiscordUser = DisUser;
            Email = email;
            EmailConfirmed = EmailConf;
            Password = Pass;
            UserRole = URole;
        }

        public string UserName { get; set; }
        public string DiscordUser { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }

    }
}
