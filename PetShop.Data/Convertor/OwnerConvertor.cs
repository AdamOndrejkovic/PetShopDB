using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas.Convertor
{
    public class OwnerConvertor
    {
        public Owner OwnerEntityConvertor(OwnerEntity ownerEntity)
        {
            return new Owner();
        }

        public OwnerEntity OwnerConvert(Owner owner)
        {
            return new OwnerEntity();
        }
    }
}