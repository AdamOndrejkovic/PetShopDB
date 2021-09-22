using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Domain.IRepositories
{
    public interface IPetTypeRepository
    {
        List<PetType> GetPetTypes();
        PetType NewPetType(string type);
        PetType UpdatePetType(int typeId, string newPetType);
        PetType GetPetTypeById(int id);
        PetType DeletePetType(int id);
    }
}