using System.Collections.Generic;
using PetShop.Core.Models;
using Filter = PetShop.Core.Filtering.Filter;

namespace PetShop.Core.IServices
{
    public interface IPetService
    {

        List<Pet> GetPets(Filter filter);
        List<Pet> GetFilteredPetsByType(string idPetType);
        Pet NewPet(string name, PetType type, string birthdate, string solddate, PetColor color, string price);
        Pet CreatePet(Pet petToBeCreated);
        Pet DeletePet(int idPet);
        Pet FindPetById(int idForEdit);
        Pet UpdatePet(Pet pet);
        List<Pet> SortByPrice(string sortOrder);
        Pet GetCheapestPets();

        int GetTotalPetCount();
    }
}