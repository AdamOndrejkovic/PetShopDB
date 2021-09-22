using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        public List<Pet> GetPets()
        {
            return _petRepository.ReadPets(null).ToList();
        }

        public List<Pet> GetFilteredPetsByType(string idPetType)
        {
            if (Int32.TryParse(idPetType, out int id))
            {
                return _petRepository.FilterPetsByType(idPetType).ToList();
            }

            return null;
        }

        public List<Pet> GetFilteredPets(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPerPage < 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPerPage must be zero or more");
            }

            if ((filter.CurrentPage - 1 * filter.ItemsPerPage) >= _petRepository.ReadPets(null).Count())
            {
                throw new InvalidDataException("Index out of bounds. Current page is to high");
            }

            return _petRepository.ReadPets(filter.CurrentPage == 0 && filter.ItemsPerPage == 0 ? null : filter).ToList();
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

        public List<Pet> GetCheapestPets()
        {
            return _petRepository.GetCheapestPets().ToList();
        }
    }
}