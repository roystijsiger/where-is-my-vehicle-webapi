using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WhereIsMyVehicle.WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }


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
