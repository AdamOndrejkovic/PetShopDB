using System.Collections.Generic;

namespace PetShop.Core.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        public List<Pet> Pets { get; set; }
    }
}