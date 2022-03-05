using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScorpioData.Dtos;
using ScorpioData.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScorpioAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;

        public UserController(IUserService userService, IWebHostEnvironment env)
        {
            _userService = userService;
            _env = env;
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

        [HttpGet("{id}/avatar")]
        public IActionResult GetUserAvatar(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(
                _env.ContentRootPath,
                $"Avatars/{user.Id}.jpg"
            );

            var image = PhysicalFile(imagePath, "image/jpeg");
            return image;
        }
    }
}

