using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas.Convertor
{
    public interface IOwnerConvertor
    {
        public Owner OwnerEntityConvertor(OwnerEntity ownerEntity);


        public OwnerEntity OwnerConvert(Owner owner);
    }
}