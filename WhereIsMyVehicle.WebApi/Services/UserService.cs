using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WhereIsMyVehicle.WebApi.Data;
using WhereIsMyVehicle.WebApi.Helpers;
using WhereIsMyVehicle.WebApi.Models;

namespace WhereIsMyVehicle.WebApi.Services
{
    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        private readonly WhereIsMyVehicleDbContext _dbContext;

        public UserService(IOptions<AppSettings> appSettings, WhereIsMyVehicleDbContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }

        public User Authenticate(string email, string password)
        {
            var user = _dbContext.Users
                .SingleOrDefault(x => x.Email == email && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(365),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}

