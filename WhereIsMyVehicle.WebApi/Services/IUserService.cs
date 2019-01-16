using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WhereIsMyVehicle.WebApi.Models;

namespace WhereIsMyVehicle.WebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
