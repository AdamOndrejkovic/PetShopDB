using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Datas.Entities
{
    public class OwnerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
        public int PetId { get; set; }
        public List<PetEntity> Pets { get; set; }
    }
}