using System.Collections.Generic;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Get(long id)
        {
            return _userRepository.GetById(id);
        }

        public User Add(User user)
        {
            return _userRepository.Add(user);
        }
    }
}