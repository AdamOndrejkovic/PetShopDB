using System.Collections.Generic;
using System.Linq;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IOwnerService
    {
        IEnumerable<Owner> GetOwners();
        Owner GetOwnerById(int id); 
        Owner CreateOwner(Owner owner);
        Owner UpdateOwner(Owner ownerToUpdate);
        Owner DeleteOwner(int id);
    }
}