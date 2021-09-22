using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IPetTypeService
    {
        List<PetType> GetPetTypes();
        PetType CreateNewPetType(string type);
        PetType UpdatePetType(int typeId, string newPetType);
        PetType GetPetTypeById(int id);
        PetType DeletePetType(int id);
    }
}