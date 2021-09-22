using System.Collections.Generic;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class PetTypeService : IPetTypeService
    {

        private readonly IPetTypeRepository _petTypeRepository;
        public PetTypeService(IPetTypeRepository petTypeRepository)
        {
            _petTypeRepository = petTypeRepository;
        }
        
        public List<PetType> GetPetTypes()
        {
            return _petTypeRepository.GetPetTypes();
        }

        public PetType CreateNewPetType(string type)
        {
            return _petTypeRepository.NewPetType(type);
        }

        public PetType UpdatePetType(int typeId, string newPetType)
        {
            return _petTypeRepository.UpdatePetType(typeId, newPetType);
        }

        public PetType GetPetTypeById(int id)
        {
            return _petTypeRepository.GetPetTypeById(id);
        }

        public PetType DeletePetType(int id)
        {
            return _petTypeRepository.DeletePetType(id);
        }
    }
}