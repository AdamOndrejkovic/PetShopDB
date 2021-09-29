using System;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas
{
    public class PetShopContext : DbContext
    {

        public PetShopContext(DbContextOptions<PetShopContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetEntity>().HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<PetEntity>().HasOne(p => p.PetType)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<PetEntity>().HasOne(p => p.Color)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            var petType = new PetTypeEntity()
            {
                Id = 1,
                Type = "Scooby-Doo"
            };
            
            modelBuilder.Entity<PetTypeEntity>()
                .HasData(petType);

            var color = new PetColorEntity(){Id = 1, Color = "Brown"} ;
            
            modelBuilder.Entity<PetColorEntity>()
                .HasData(color);

            var owner = new OwnerEntity()
            {
                Id = 1,
                FirstName = "Velma",
                LastName = "Dinkley",
                Email = "velma@yahoo.fr",
                PhoneNumber = "35 23 63 34"
            };
            
            modelBuilder.Entity<OwnerEntity>()
                .HasData(owner);
            for (int i = 1; i < 1000; i++)
            {
                modelBuilder.Entity<PetEntity>()
                    .HasData(new PetEntity()
                    {
                        Id = i,
                        Name = "Scooby-Doo" + i,
                        PetTypeId = 1,
                        Birthdate = new DateTime(2007, 5, 1, 8, 30, 52),
                        SoldDate = new DateTime(2008, 7, 8, 8, 30, 52),
                        ColorId = 1,
                        Price = 250,
                        OwnerId = 1,
                    });
            }
        }

        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<PetTypeEntity> PetTypes { get; set; }
        public DbSet<OwnerEntity> Owners { get; set; }
    }
}