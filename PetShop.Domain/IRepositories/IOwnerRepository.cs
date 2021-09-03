using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Domain.IRepositories
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetOwners();
        Owner GetOwnerById(int id);
        Owner CreateOwner(Owner owner);
        Owner UpdateOwner(Owner ownerToUpdate);
        Owner DeleteOwner(int id);
    }
}