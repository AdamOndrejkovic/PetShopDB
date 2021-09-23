using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User Get(long id);
        User Add(User user);
    }
}