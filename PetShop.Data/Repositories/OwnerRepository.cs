using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;
using PetShop.Datas.Convertor;
using PetShop.Datas.Entities;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class OwnerRepository: IOwnerRepository, IOwnerConvertor
    {
        
        private readonly PetShopContext _context;

        public OwnerRepository(PetShopContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Owner> GetOwners()
        {
            return _context.Owners
                .Include(o => o.Pets)
                .ThenInclude(p => p.PetType)
                .Select(OwnerEntityConvertor).ToList();
        }

        public Owner GetOwnerById(int id)
        {
            var owner = _context.Owners
                    .Include(o => o.Pets)
                    .ThenInclude(p => p.PetType)
                    .FirstOrDefault(owner => owner.Id == id);
            return OwnerEntityConvertor(owner);
        }
        
        public Owner GetOwnerByIdWithPet(int id)
        {
            /*return from owner in _context.Owners
                join pet in _context.Pets on owner.Id equals pet.Owner.Id
                select owner;*/

            var owner = _context.Owners.FirstOrDefault(owner => owner.Id == id);
            return OwnerEntityConvertor(owner);
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
            return OwnerEntityConvertor(ownerToRemove);
        }

        public Owner OwnerEntityConvertor(OwnerEntity ownerEntity)
        {
            return new Owner()
            {
                Id = ownerEntity.Id,
                FirstName = ownerEntity.FirstName,
                LastName = ownerEntity.LastName,
                Email = ownerEntity.Email,
                PhoneNumber = ownerEntity.PhoneNumber,
                //Pets = ownerEntity.Pets
            };
        }

        public OwnerEntity OwnerConvert(Owner owner)
        {
            return new OwnerEntity()
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber,
                //Pets = owner.Pets
            };
        }
    }
}