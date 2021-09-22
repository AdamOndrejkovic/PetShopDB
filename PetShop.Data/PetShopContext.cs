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
            
            modelBuilder.Entity<PetEntity>().HasOne(p => p.Type)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<PetEntity>().HasOne(p => p.Color)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            
        }

        public DbSet<PetEntity> Pets { get; set; }
        public DbSet<PetTypeEntity> PetTypes { get; set; }
        public DbSet<OwnerEntity> Owners { get; set; }
    }
}