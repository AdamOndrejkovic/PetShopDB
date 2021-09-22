using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;
using PetShop.Datas.Convertor;
using PetShop.Datas.Entities;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        
        private readonly PetShopContext _context;
        private readonly OwnerConvertor _ownerConvertor;

        public OwnerRepository(PetShopContext context, OwnerConvertor ownerConvertor)
        {
            _context = context;
            _ownerConvertor = ownerConvertor;
        }
        
        public IEnumerable<Owner> GetOwners()
        {
            var owner = _context.Owners
                .Include(o => o.Pets)
                .ThenInclude(p => p.Type);
            return _ownerConvertor.OwnerEntityConvertor(owner);
        }

        public Owner GetOwnerById(int id)
        {
            var owner = _context.Owners
                    .Include(o => o.Pets)
                    .ThenInclude(p => p.Type)
                    .FirstOrDefault(owner => owner.Id == id);
            return _ownerConvertor.OwnerEntityConvertor(owner);
        }
        
        public Owner GetOwnerByIdWithPet(int id)
        {
            /*return from owner in _context.Owners
                join pet in _context.Pets on owner.Id equals pet.Owner.Id
                select owner;*/

            var owner = _context.Owners.FirstOrDefault(owner => owner.Id == id);
            return _ownerConvertor.OwnerEntityConvertor(owner);
        }

        public Owner CreateOwner(Owner owner)
        {
            _context.Attach(owner).State = EntityState.Added;
            _context.SaveChanges();
            return owner;
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            _context.Attach(ownerToUpdate).State = EntityState.Modified;
            _context.Entry(ownerToUpdate).Reference(o => o.Pets).IsModified = true;
            _context.SaveChanges();
            return ownerToUpdate;
        }

        public Owner DeleteOwner(int id)
        {
            var ownerToRemove = _context.Owners.Remove(new OwnerEntity() { Id = id }).Entity;
            _context.SaveChanges();
            return _ownerConvertor.OwnerEntityConvertor(ownerToRemove);
        }
    }
}