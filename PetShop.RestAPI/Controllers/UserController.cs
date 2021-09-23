using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetShop.Core.IServices;
using PetShop.Core.Models;

namespace PetShop.RestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetAll();
        }

        [HttpGet("{id:long}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var item = _userService.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            
            _userService.Add(user);
            return CreatedAtRoute("Get", new { id = user.Id }, user);
        }

    }
}