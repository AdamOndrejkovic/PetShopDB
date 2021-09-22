using System.Collections.Generic;
using System.Linq;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class PetTypeRepository: IPetTypeRepository
    {
        
        private readonly PetShopContext _context;

        public PetTypeRepository(PetShopContext context)
        {
            _context = context;
        }
        
        public List<PetType> GetPetTypes()
        {
            return _context.PetTypes.ToList();
        }

        public PetType NewPetType(string type)
        {
            return _context.PetTypes.Add(new PetType(){Name = type}).Entity;
        }

        public PetType UpdatePetType(int typeId, string newPetType)
        {
            throw new System.NotImplementedException();
        }

        public PetType GetPetTypeById(int id)
        {
            return _context.PetTypes.FirstOrDefault(pet => pet.Id == id);
        }

        public PetType DeletePetType(int id)
        {
            var petTypeRemove = _context.Remove<PetType>(new PetType() { Id = id }).Entity;
            var petToRemove = _context.Pets.Where(pet => pet.Type.Id == petTypeRemove.Id);
            _context.RemoveRange(petTypeRemove);
            _context.SaveChanges();
            return petTypeRemove; 
        }
    }
}