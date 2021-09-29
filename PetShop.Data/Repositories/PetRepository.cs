using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Filtering;
using PetShop.Core.Models;
using PetShop.Datas.Convertor;
using PetShop.Datas.Entities;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class PetRepository : IPetRepository, IPetConvertor
    {
        private readonly PetShopContext _context;

        public PetRepository(PetShopContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Pet> ReadPets(Filter filter)
        {
            
            var selectQuery = _context.Pets.
                Select(petEntity => new Pet()
                {
                    Id = petEntity.Id,
                    Name = petEntity.Name,
                    Birthdate = petEntity.Birthdate,
                    SoldDate = petEntity.SoldDate,
                    Type = new PetType()
                    {
                        Id = petEntity.PetType.Id,
                        Name = petEntity.PetType.Type,
                    },
                    Color = new PetColor()
                    {
                        Id = petEntity.Color.Id,
                        Color = petEntity.Color.Color
                    },
                    Price = petEntity.Price,
                    Owner = new Owner()
                    {
                        Id = petEntity.Owner.Id,
                        FirstName = petEntity.Owner.FirstName,
                        LastName = petEntity.Owner.LastName,
                        PhoneNumber = petEntity.Owner.PhoneNumber,
                        Email = petEntity.Owner.Email,
                    }
                
                });
            
            var paging = selectQuery.
                Skip(filter.Count * (filter.Page - 1)).
                Take(filter.Count);

            if (string.IsNullOrEmpty(filter.Sortorder) || filter.Sortorder.Equals("asc"))
            {
                paging = filter.SortBy switch
                {
                    "id" => paging.OrderBy(p => p.Id),
                    "name" => paging.OrderBy(p => p.Name),
                    "price" => paging.OrderBy(p => p.Price),
                    _ => paging.OrderBy(p => p.Name)
                };
            }
            else
            {
                paging = filter.SortBy switch
                {
                    "id" => paging.OrderBy(p => p.Id),
                    "name" => paging.OrderBy(p => p.Name),
                    "price" => paging.OrderBy(p => p.Price),
                    _ => paging.OrderBy(p => p.Name)
                };
            }

            if (!string.IsNullOrEmpty(filter.Search))
            {
                paging = paging.Where(p => p.Name.ToLower().Contains(filter.Search.ToLower()));
            }

            return paging.ToList();

        }

        public IEnumerable<Pet> FilterPetsByType(string idPetType)
        {
            throw new System.NotImplementedException();
        }

        public Pet CreatePet(Pet petToBeCreated)
        {
            var petEntity = PetConvert(petToBeCreated);
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
            return _context.Pets.Select(PetEntityConvertor).FirstOrDefault(pet => pet.Id == idForEdit);
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

        public Pet GetCheapestPets()
        {
            return ReadPets(null).Min();
        }

        public int TotalPetCount()
        {
            return _context.Pets.Count();
        }

        public Pet PetEntityConvertor(PetEntity petEntity)
        {
            return new Pet()
            {
                Id = petEntity.Id,
                Name = petEntity.Name,
                Birthdate = petEntity.Birthdate,
                SoldDate = petEntity.SoldDate,
                Type = new PetType()
                {
                    Id = petEntity.PetType.Id,
                    Name = petEntity.PetType.Type,
                },
                Color = new PetColor()
                {
                    Id = petEntity.Color.Id,
                    Color = petEntity.Color.Color
                },
                Price = petEntity.Price,
                Owner = new Owner()
                {
                    Id = petEntity.Owner.Id,
                    FirstName = petEntity.Owner.FirstName,
                    LastName = petEntity.Owner.LastName,
                    PhoneNumber = petEntity.Owner.PhoneNumber,
                    Email = petEntity.Owner.Email,
                }
                
            };
        }

        public PetEntity PetConvert(Pet pet)
        {
            return new PetEntity();
        }
    }
}