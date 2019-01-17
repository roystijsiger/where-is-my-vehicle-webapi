using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WhereIsMyVehicle.WebApi.Extensions;

namespace WhereIsMyVehicle.WebApi.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public string AvatarUrl => $"https://www.gravatar.com/avatar/{Email.CalculateMd5Hash()}?d=retro";

        public User()
        {
        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

    }
}
