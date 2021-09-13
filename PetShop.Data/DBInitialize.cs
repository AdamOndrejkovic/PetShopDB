using System;
using PetShop.Core.Models;

namespace PetShop.Data
{
    public class DbInitialize
    {
        public static void InitData(PetShopContext context)
        {
            context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                    
                    var petType = context.PetTypes.Add(new PetType()
                    {
                        Name = "Some weird animal"
                    }).Entity;

                    var petTypeTwo = context.PetTypes.Add(new PetType()
                    {
                        Name = "Scooby-Doo"
                    }).Entity;

                    var owner = context.Owners.Add(new Owner()
                    {
                        FirstName = "Velma",
                        LastName = "Dinkley",
                        Email = "velma@yahoo.fr",
                        PhoneNumber = "35 23 63 34"
                    }).Entity;

                    var ownerTwo = context.Owners.Add(new Owner()
                    {
                        FirstName = "Fred",
                        LastName = "Jones",
                        Email = "fred@yahoo.fr",
                        PhoneNumber = "35 23 63 34"
                    }).Entity;

                    context.Pets.Add(new Pet()
                    {
                        Name = "Scrappy-Doo",
                        Type = petType,
                        Birthdate = new DateTime(2006, 5, 1, 8, 30, 52),
                        SoldDate = new DateTime(2008, 7, 8, 8, 30, 52),
                        Color = "Blonde",
                        Price = 200,
                        Owner = owner
                    });

                    context.Pets.Add(new Pet()
                    {
                        Name = "Scooby-Doo",
                        Type = petTypeTwo,
                        Birthdate = new DateTime(2007, 5, 1, 8, 30, 52),
                        SoldDate = new DateTime(2008, 7, 8, 8, 30, 52),
                        Color = "Blonde",
                        Price = 250,
                        Owner = ownerTwo
                    });

                    context.SaveChanges();
        }
    }
}