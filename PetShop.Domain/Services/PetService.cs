using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PetShop.Core.Filtering;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class PetService: IPetService
    {

        private readonly IPetRepository _petRepository;
        
        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }
        
        public List<Pet> GetPets(Filter filter)
        {
            if (filter.Count is <= 0 or > 500)
            {
                throw new ArgumentException("You need to put a filter count between 1 and 500");
            }

            var totalCount = _petRepository.TotalPetCount();
            if (filter.Count * (filter.Page - 1) > totalCount)
            {
                throw new ArgumentException("An error occured");
            }
            
            
            return _petRepository.ReadPets(filter).ToList();
        }

        public List<Pet> GetFilteredPetsByType(string idPetType)
        {
            if (Int32.TryParse(idPetType, out int id))
            {
                return _petRepository.FilterPetsByType(idPetType).ToList();
            }

            return null;
        }

        public Pet NewPet(string name, PetType type, string birthdate, string solddate, PetColor color, string price)
        {
            string[] formatedBrithdate = birthdate.Split("/");
            string[] formatedSolddate = solddate.Split("/");
            return new Pet
            {
                Name = name,
                Type = type,
                Birthdate =  new DateTime(Convert.ToInt32(formatedBrithdate[2]), Convert.ToInt32(formatedBrithdate[1]), Convert.ToInt32(formatedBrithdate[0])),
                SoldDate  =  new DateTime(Convert.ToInt32(formatedSolddate[2]), Convert.ToInt32(formatedSolddate[1]), Convert.ToInt32(formatedSolddate[0])),
                Color = color,
                Price = double.Parse(price)
            };
        }

        public Pet CreatePet(Pet petToBeCreated)
        {
            return _petRepository.CreatePet(petToBeCreated);
        }

        public Pet DeletePet(int idPet)
        {
            return _petRepository.DeletePet(idPet);
        }

        public Pet FindPetById(int idForEdit)
        {
            return _petRepository.FindPetById(idForEdit);
        }

        public Pet UpdatePet(Pet pet)
        {
           return _petRepository.UpdatePet(pet);
        }

        public List<Pet> SortByPrice(string sortOrder)
        {
            return _petRepository.SortByPrice(sortOrder);
        }

        public Pet GetCheapestPets()
        {
            return _petRepository.GetCheapestPets();
        }

        public int GetTotalPetCount()
        {
            return _petRepository.TotalPetCount();
        }
    }
}