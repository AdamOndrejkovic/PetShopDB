using System.Collections.Generic;
using System.Linq;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetShopContext _context;

        public PetRepository(PetShopContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Pet> ReadPets()
        {
            return _context.Pets;
        }

        public IEnumerable<Pet> FilterPetsByType(string idPetType)
        {
            throw new System.NotImplementedException();
        }

        public Pet CreatePet(Pet petToBeCreated)
        {
            var pet = _context.Pets.Add(petToBeCreated).Entity;
            _context.SaveChanges();
            return pet;
        }

        public Pet DeletePet(int idPet)
        {
            var petRemoved = _context.Remove<Pet>(new Pet() { Id = idPet }).Entity;
            var ownersToRemove = _context.Owners.Where(owner => owner.Id == petRemoved.Owner.Id);
            _context.RemoveRange(ownersToRemove);
            _context.SaveChanges();
            return petRemoved;

        }

        public Pet FindPetById(int idForEdit)
        {
            return _context.Pets.FirstOrDefault(pet => pet.Id == idForEdit);
        }

        public Pet UpdatePet(Pet pet)
        {
            throw new System.NotImplementedException();
        }

        public List<Pet> SortByPrice(string sortOrder)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Pet> GetCheapestPets()
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}