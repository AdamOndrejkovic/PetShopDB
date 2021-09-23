using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.Helpers;
using PetShop.Core.IServices;
using PetShop.Core.Models;

namespace PetShop.RestAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;
        private IAuthenticationHelper _authHelper;

        public LoginController(IUserService userService, IAuthenticationHelper authHelper)
        {
            _userService = userService;
            _authHelper = authHelper;
        }

        // POST: api/Login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] LoginInput model)
        {
            User user = _userService.GetAll().FirstOrDefault(user => user.Username.Equals(model.Username));

            //Did we find a user with the given username?
            if (user == null)
                return Unauthorized();

            //Was the correct password given?
            if (!_authHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            //Authentication succesful
            return Ok(new
            {
                username = user.Username,
                token = _authHelper.GenerateToken(user)
            });
        }
    }
}