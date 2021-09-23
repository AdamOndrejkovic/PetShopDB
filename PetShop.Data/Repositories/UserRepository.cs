using System.Collections.Generic;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User GetById(long id)
        {
            throw new System.NotImplementedException();
        }

        public User Add(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}