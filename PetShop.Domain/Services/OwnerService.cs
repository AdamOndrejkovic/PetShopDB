using System.Collections.Generic;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        
        public IEnumerable<Owner> GetOwners()
        {
            return _ownerRepository.GetOwners();
        }

        public Owner GetOwnerById(int id)
        {
            return _ownerRepository.GetOwnerByIdWithPet(id);

            //return _ownerRepository.GetOwnerById(id);
        }

        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepository.CreateOwner(owner);
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            return _ownerRepository.UpdateOwner(ownerToUpdate);
        }

        public Owner DeleteOwner(int id)
        {
            return _ownerRepository.DeleteOwner(id);
        }
    }
}