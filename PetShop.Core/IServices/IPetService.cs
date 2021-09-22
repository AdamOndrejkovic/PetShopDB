using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IPetService
    {

        List<Pet> GetPets();
        List<Pet> GetFilteredPetsByType(string idPetType);
        List<Pet> GetFilteredPets(Filter filter = null);
        Pet NewPet(string name, PetType type, string birthdate, string solddate, PetColor color, string price);
        Pet CreatePet(Pet petToBeCreated);
        Pet DeletePet(int idPet);
        Pet FindPetById(int idForEdit);
        Pet UpdatePet(Pet pet);
        List<Pet> SortByPrice(string sortOrder);
        List<Pet> GetCheapestPets();
        
    }
}