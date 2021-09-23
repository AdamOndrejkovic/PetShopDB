using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Domain.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(long id);
        User Add(User user);
    }
}