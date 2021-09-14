using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;

namespace PetShop.Datas
{
    public class PetShopContext : DbContext
    {

        public PetShopContext(DbContextOptions<PetShopContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(pet => pet.Owner)
                .WithMany(owner => owner.Pets)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}