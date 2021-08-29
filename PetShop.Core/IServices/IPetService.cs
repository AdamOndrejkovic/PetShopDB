using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IPetService
    {

        List<Pet> GetPets();
        List<Pet> GetFilteredPetsByType(string idPetType);
        Pet NewPet(string name, PetType type, string birthdate, string solddate, string color, string price);
        void CreatePet(Pet petToBeCreated);
        Pet DeletePet(int idPet);
        Pet FindPetById(int idForEdit);
        Pet UpdatePet(Pet pet);
        List<Pet> SortByPrice(string sortOrder);
        List<Pet> GetCheapestPets();
    }
}