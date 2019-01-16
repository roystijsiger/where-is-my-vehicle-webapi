using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhereIsMyVehicle.WebApi.Data;
using WhereIsMyVehicle.WebApi.Models;
using WhereIsMyVehicle.WebApi.Services;

namespace WhereIsMyVehicle.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly WhereIsMyVehicleDbContext _dbContext;

        public UsersController(IUserService userService, WhereIsMyVehicleDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _dbContext.Users.Select(x => new User
                {
                    Id = x.Id,
                    Email = x.Email
                }
            );

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]CredentialsModel credentials)
        {
            try
            {
                new MailAddress(credentials.Email);

                if (credentials.Password.Length < 6) throw new ArgumentException("password");
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var user = _dbContext.Users.Add(new User(credentials.Email, credentials.Password)).Entity;
            await _dbContext.SaveChangesAsync();

            user.Password = null;

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult Token([FromBody]CredentialsModel credentials)
        {
            try
            {
                new MailAddress(credentials.Email);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var user = _userService.Authenticate(credentials.Email, credentials.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}