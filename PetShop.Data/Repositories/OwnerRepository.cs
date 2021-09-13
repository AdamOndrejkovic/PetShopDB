using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Data.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        
        private readonly PetShopContext _context;

        public OwnerRepository(PetShopContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Owner> GetOwners()
        {
            return _context.Owners;
        }

        public Owner GetOwnerById(int id)
        {
            return _context.Owners.FirstOrDefault(owner => owner.Id == id);
        }
        
        public Owner GetOwnerByIdWithPet(int id)
        {
            /*return from owner in _context.Owners
                join pet in _context.Pets on owner.Id equals pet.Owner.Id
                select owner;*/

            return _context.Owners.FirstOrDefault(owner => owner.Id == id);
        }

        public Owner CreateOwner(Owner owner)
        {
            return _context.Owners.Add(owner).Entity;
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public Owner DeleteOwner(int id)
        {
            var ownerRemove = _context.Owners.Remove(new Owner() { Id = id }).Entity;
            _context.SaveChanges();
            return ownerRemove;
        }
    }
}