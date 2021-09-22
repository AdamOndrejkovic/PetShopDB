﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;
using PetShop.Datas.Convertor;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetShopContext _context;
        private readonly PetConvertor _convertorPet;

        public PetRepository(PetShopContext context, PetConvertor petConvertor)
        {
            _context = context;
            _convertorPet = petConvertor;
        }
        
        public IEnumerable<Pet> ReadPets(Filter filter)
        {
            if (filter == null)
            {
                return _context.Pets.Include(p => p.Owner).Include(p => p.Type)
                    .Select(pe => new Pet()
                    {
                        
                    });
            }

            return _context.Pets
                .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage)
                .Select(pe => new Pet()
                {
                    
                });

        }

        public IEnumerable<Pet> FilterPetsByType(string idPetType)
        {
            throw new System.NotImplementedException();
        }

        public Pet CreatePet(Pet petToBeCreated)
        {
            /*var pet = _context.Pets.Add(petToBeCreated).Entity;
            var ownerOfPet = _context.Owners.Where(owner => owner.Id == pet.Id);
            _context.Owners.Find(ownerOfPet).Pets.Add(pet);
            _context.SaveChanges();
            return pet;*/
            var petEntity = _convertorPet.PetConvert(petToBeCreated);
            _context.Pets.Attach(petEntity).State = EntityState.Added;
            _context.SaveChanges();
            return petToBeCreated;
        }

        public Pet DeletePet(int idPet)
        {
            var petRemoved = _context.Remove<Pet>(new Pet() { Id = idPet }).Entity;
            var ownersToRemove = _context.Owners.Where(owner => owner.Id == petRemoved.Owner.Id);
            _context.RemoveRange(ownersToRemove);
            _context.SaveChanges();
            return petRemoved;

        }

        public Pet FindPetById(int idForEdit)
        {
            return _context.Pets.Select(p => new Pet(){}).FirstOrDefault(pet => pet.Id == idForEdit);
        }

        public Pet UpdatePet(Pet pet)
        {
            _context.Attach(pet).State = EntityState.Modified;
            _context.Entry(pet).Reference(p => p.Owner).IsModified = true;
            _context.SaveChanges();
            return pet;
        }

        public List<Pet> SortByPrice(string sortOrder)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Pet> GetCheapestPets()
        {
            throw new System.NotImplementedException();
        }
    }
}