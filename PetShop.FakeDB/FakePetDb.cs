using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.SQL
{
    public class PetDb : IPetRepository, IPetTypeRepository, IOwnerRepository
    {
        private static List<Pet> _petList = new();
        private static List<PetType> _petTypeList = new();
        private static int _petId = 4;
        private static int _petTypeId = 4;
        private static List<Owner> _ownersList = new();
        private static int _ownerId = 3;

        public PetDb()
        { }


        public IEnumerable<Pet> ReadPets()
        {
            return _petList;
        }

        public IEnumerable<Pet> FilterPetsByType(string idPetType)
        {
            int typeId = int.Parse(idPetType);
            var listToFilter = from pet in _petList where pet.Type.Id.Equals(typeId) select pet;
            return listToFilter;
        }

        public Pet CreatePet(Pet petToBeCreated)
        {
            petToBeCreated.Id = _petId++;
            _petList.Add(petToBeCreated);
            return petToBeCreated;
        }

        public Pet DeletePet(int idPet)
        {
            var petToFind = FindPetById(idPet);
            if (petToFind != null)
            {
                _petList.Remove(petToFind);
                return petToFind;
            }
            return null;
        }

        public Pet FindPetById(int idForEdit)
        {
            foreach (var pet in _petList)
            {
                if (pet.Id == idForEdit)
                {
                    return pet;
                }
            }

            return null;
        }

        public Pet UpdatePet(Pet pet)
        {
            var findPetToBeEdited = FindPetById(pet.Id);
            if(findPetToBeEdited != null)
            {
                findPetToBeEdited.Name = pet.Name;
                findPetToBeEdited.Type = pet.Type;
                findPetToBeEdited.Birthdate = pet.Birthdate;
                findPetToBeEdited.SoldDate = pet.SoldDate;
                findPetToBeEdited.Color = pet.Color;
                findPetToBeEdited.Price = pet.Price;
                return findPetToBeEdited;
            }

            return null;
        }

        public List<Pet> SortByPrice(string sortOrder)
        {
            IOrderedEnumerable<Pet> orderedPetList = null;
            if (Int32.TryParse(sortOrder, out int number))
            {
                if (number == 1)
                {
                    orderedPetList = from pet in _petList orderby pet.Price ascending select pet;
                }
                else
                {
                     orderedPetList = from pet in _petList orderby pet.Price descending select pet;
                }
            }
            
            return orderedPetList.ToList();
        }

        public IEnumerable<Pet> GetCheapestPets()
        {
            var filteredCheapestPets = from pet in _petList orderby pet.Price ascending select pet;
            return filteredCheapestPets.ToList().Take(5);
        }

        public void Init()
        {
            _petTypeList.Add(new PetType() {Id = 1, Name = "Brown dog"});
            _petTypeList.Add(new PetType() {Id = 2, Name = "Egyptian Cat"});
            _petTypeList.Add(new PetType() {Id = 3, Name = "Hungry Goat"});
            
            _ownersList.Add(new Owner()
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Marley",
                Email = "marley@yahoo.fr",
                PhoneNumber = "35 23 63 34"
            });
            
            _ownersList.Add(new Owner()
            {
                Id = 2,
                FirstName = "Ellen",
                LastName = "Mackenzie",
                Email = "ellen@gmail.dk",
                PhoneNumber = "46 93 12 02"
            });
            
            _petList.Add(new Pet()
            {
                Id = 1, Name = "Marcus",
                Type = _petTypeList[0], Birthdate = new DateTime(2008, 5, 1, 8, 30, 52),
                SoldDate = new DateTime(2008, 7, 8, 8, 30, 52), Color = "Dark Brown", Price = 300,
                Owner = _ownersList[0]
            });
            _petList.Add(new Pet()
            {
                Id = 2, Name = "Margot",
                Type = _petTypeList[1], Birthdate = new DateTime(2006, 5, 1, 8, 30, 52),
                SoldDate = new DateTime(2008, 7, 8, 8, 30, 52), Color = "Blonde", Price = 200,
                Owner = _ownersList[1]
            });
            _petList.Add(new Pet()
            {
                Id = 3, Name = "Charles",
                Type = _petTypeList[2], Birthdate = new DateTime(2007, 5, 1, 8, 30, 52),
                SoldDate = new DateTime(2008, 7, 8, 8, 30, 52), Color = "White", Price = 100,
                Owner = _ownersList[0]
            });
        }

        public List<PetType> GetPetTypes()
        {
            return _petTypeList;
        }

        public PetType NewPetType(string type)
        {
            PetType petTypeToBeCreated = new PetType() {Id = _petTypeId++, Name = type};
            return petTypeToBeCreated;
        }

        public PetType UpdatePetType(int typeId, string newPetType)
        {
            var petTypeToEdit = FindPetTypeById(typeId);
            if (petTypeToEdit != null)
            {
                petTypeToEdit.Name = newPetType;
                return petTypeToEdit;
            }

            return null;
        }

        private PetType FindPetTypeById(int typeId)
        {
            foreach (var petType in _petTypeList)
            {
                if (petType.Id == typeId)
                {
                    return petType;
                }
            }
            return null;
        }

        public IEnumerable<Owner> GetOwners()
        {
            return _ownersList;
        }

        public Owner GetOwnerById(int id)
        {
            foreach (var owner in _ownersList)
            {
                if (owner.Id == id)
                {
                    return owner;
                }
            }

            return null;
        }

        public Owner CreateOwner(Owner ownerToBeCreated)
        {
            ownerToBeCreated.Id = _ownerId++;
            _ownersList.Add(ownerToBeCreated);
            return ownerToBeCreated;
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            Owner findOwner = GetOwnerById(ownerToUpdate.Id);
            if (findOwner != null)
            {
                findOwner.FirstName = ownerToUpdate.FirstName;
                findOwner.LastName = ownerToUpdate.LastName;
                findOwner.Email = ownerToUpdate.Email;
                findOwner.PhoneNumber = ownerToUpdate.PhoneNumber;

                return findOwner;
            }

            return null;
        }

        public Owner DeleteOwner(int id)
        {
            Owner ownerToDelete = GetOwnerById(id);
            if (ownerToDelete != null)
            {
                _ownersList.Remove(ownerToDelete);
                return ownerToDelete;
            }

            return null;
        }

        public Owner GetOwnerByIdWithPet(int id)
        {
            throw new NotImplementedException();
        }
    }
}