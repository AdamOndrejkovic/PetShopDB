using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;
using PetShop.Datas.Convertor;
using PetShop.Datas.Entities;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class PetTypeRepository: IPetTypeRepository, IPetTypeConvertor
    {
        
        private readonly PetShopContext _context;

        public PetTypeRepository(PetShopContext context)
        {
            _context = context;
        }
        
        public List<PetType> GetPetTypes()
        {
            return _context.PetTypes
                .Select(ConvertToPetType).ToList();;
        }

        public PetType NewPetType(string type)
        {
            var newPetType = _context.PetTypes.Add(new PetTypeEntity(){Type = type});
            return ConvertToPetType(newPetType.Entity);
        }

        public PetType UpdatePetType(int typeId, string newPetType)
        {
            var petType = GetPetTypeById(typeId);
            petType.Name = newPetType;
            _context.Attach(petType).State = EntityState.Modified;
            _context.SaveChanges();
            return petType;
        }

        public PetType GetPetTypeById(int id)
        {
            var petType = _context.PetTypes.FirstOrDefault(pet => pet.Id == id);
            return ConvertToPetType(petType);
        }

        public PetType DeletePetType(int id)
        {
            var petTypeRemove = _context.Remove<PetType>(new PetType() { Id = id }).Entity;
            var petToRemove = _context.Pets.Where(pet => pet.PetType.Id == petTypeRemove.Id);
            _context.RemoveRange(petTypeRemove);
            _context.SaveChanges();
            return petTypeRemove; 
        }
        
        public PetEntity ConvertToPetTypeEntity(PetType petType)
        {
            return new PetEntity();
        }

        public PetType ConvertToPetType(PetTypeEntity petEntity)
        {
            return new PetType();
        }
    }
}