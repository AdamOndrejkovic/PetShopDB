using System.Linq;
using PetShop.Core.Helpers;
using PetShop.Core.Models;
using PetShop.Datas.Repositories;

namespace PetShop.Security.Helpers
{

    public interface IUserAuthenticator
    {
        bool Login(string username, string password, out string token);
        bool CreateUser(string username, string password);
    }
    
    public class UserAuthenticator : IUserAuthenticator
    {

        private readonly UserRepository _userRepository;
        private readonly IAuthenticationHelper _authenticationHelper;

        public UserAuthenticator(UserRepository userRepository, IAuthenticationHelper authenticationHelper)
        {
            _userRepository = userRepository;
            _authenticationHelper = authenticationHelper;
        }

        public bool Login(string username, string password, out string token)
        {
            User user = _userRepository.GetAll().FirstOrDefault(user => user.Username.Equals(username));

            if (user == null)
            {
                token = null;
                return false;
            }

            if (!_authenticationHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                token = null;
                return false;
            }

            token = _authenticationHelper.GenerateToken(user);
            return true;

        }

        public bool CreateUser(string username, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                return false;
            }

            byte[] salt;
            byte[] passwordHash;
            _authenticationHelper.CreatePasswordHash(password, out passwordHash, out salt);

            user = new User()
            {
                Username = username,
                Role = "User",
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                IsAdmin = false
            };

            _userRepository.Add(user);

            return true;
        }
    }
}