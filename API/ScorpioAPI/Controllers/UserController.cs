using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScorpioData.Dtos;
using ScorpioData.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScorpioAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public UserDto Get()
        {
            return _userService.Me();
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> Get(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}

