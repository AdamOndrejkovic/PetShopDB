using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetShop.Core.Helpers;
using PetShop.Core.IServices;
using PetShop.Core.Models;

namespace PetShop.RestAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class RegisterUserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthenticationHelper _authentication;
        
        public RegisterUserController(IUserService userService, IAuthenticationHelper authHelper, ILogger<UserController> logger)
        {
            _logger = logger;
            _userService = userService;
            _authentication = authHelper;
        }
        
        // POST: api/Login
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserModel model)
        {
            User user = _userService.GetAll().FirstOrDefault(u => u.Username == model.Username);

            //Does already contain a user with the given username?
            if (user != null)
                return Unauthorized();

            byte[] salt; 
            byte[] passwordHash;
            _authentication.CreatePasswordHash(model.Password, out passwordHash, out salt);

            user = new User()
            {
                Username = model.Username,
                IsAdmin = false,
                PasswordHash = passwordHash,
                PasswordSalt = salt
            };

            _userService.Add(user);
            //I get a fresh object from the db (With an ID):
            user = _userService.GetAll().FirstOrDefault(u => u.Username == model.Username);
            
            //Authentication succesful
            return Ok(new
            {
                username = user.Username,
                token = _authentication.GenerateToken(user)
            });
            
        }
    }
}