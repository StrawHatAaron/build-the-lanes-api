using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using BuildTheLanesAPI.Services;
using BuildTheLanesAPI.Entities;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Constants.api + "/[controller]")]
    public class UsersController : ControllerBase
    {

        public IConfiguration Configuration { get; }
        private readonly string connectionString;
        private IUserService _userService;

        public UsersController(IConfiguration configuration, IUserService userService)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _userService = userService;
        }

        

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(user);
        }


        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Console.WriteLine("Getting User id:" + id);
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Roles.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}