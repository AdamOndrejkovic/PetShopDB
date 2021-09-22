using PetShop.Core.Models;

namespace PetShop.RestAPI.Dto
{
    public class PetCreateDto
    {
        public string Name { get; set; }
        public int PetTypeId { get; set; }
        public int OwnerId { get; set; }
    }
}