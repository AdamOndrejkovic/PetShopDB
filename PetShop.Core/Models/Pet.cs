using System;

namespace PetShop.Core.Models
{
    public class Pet
    {
        public int Id { get; set;}
        public string Name {get; set;}
        public PetType Type {get; set;}
        public DateTime Birthdate {get; set;}
        public DateTime SoldDate {get; set;}
        public string Color {get; set;}
        public double Price {get; set;}
    }
}