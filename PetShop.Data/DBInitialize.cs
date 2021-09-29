using System;
using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas
{
    public class DbInitialize
    {
        public static void InitData(PetShopContext context)
        {
            context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    
                    /*var petType = context.PetTypes.Add(new PetTypeEntity()
                    {
                        Id = 1,
                        Type = "Some weird animal"
                    }).Entity;

                    var petTypeTwo = context.PetTypes.Add(new PetTypeEntity()
                    {
                        Id = 2,
                        Type = "Scooby-Doo"
                    }).Entity;

                    var owner = context.Owners.Add(new OwnerEntity()
                    {
                        FirstName = "Velma",
                        LastName = "Dinkley",
                        Email = "velma@yahoo.fr",
                        PhoneNumber = "35 23 63 34"
                    }).Entity;

                    var ownerTwo = context.Owners.Add(new OwnerEntity()
                    {
                        FirstName = "Fred",
                        LastName = "Jones",
                        Email = "fred@yahoo.fr",
                        PhoneNumber = "35 23 63 34"
                    }).Entity;

                    var pet= context.Pets.Add(new PetEntity()
                    {
                        Name = "Scrappy-Doo",
                        Type = petType,
                        Birthdate = new DateTime(2006, 5, 1, 8, 30, 52),
                        SoldDate = new DateTime(2008, 7, 8, 8, 30, 52),
                        Color = new PetColorEntity(){Id = 1, Color = "Blonde"},
                        Price = 200,
                        Owner = owner
                    }).Entity;

                    var petTwo = context.Pets.Add(new PetEntity()
                    {
                        Name = "Scooby-Doo",
                        Type = petTypeTwo,
                        Birthdate = new DateTime(2007, 5, 1, 8, 30, 52),
                        SoldDate = new DateTime(2008, 7, 8, 8, 30, 52),
                        Color = new PetColorEntity(){Id = 2, Color = "Brown"},
                        Price = 250,
                        Owner = ownerTwo
                    }).Entity;

                    owner.Pets.Add(pet);
                    ownerTwo.Pets.Add(petTwo);
                    
                    context.SaveChanges();*/
        }
    }
}