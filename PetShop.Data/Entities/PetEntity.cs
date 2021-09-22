using System;
using PetShop.Core.Models;

namespace PetShop.Datas.Entities
{
    public class PetEntity
    {
        public int Id { get; set;}
        public string Name {get; set;}
        public int PetTypeId { get; set; }
        public PetTypeEntity Type {get; set;}
        public DateTime Birthdate {get; set;}
        public DateTime SoldDate {get; set;}
        public int ColorId { get; set; }
        public PetColorEntity Color {get; set;}
        public double Price {get; set;}
        public int OwnerId { get; set; }

        public OwnerEntity Owner { get; set; }
    }
}