using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas.Convertor
{
    public interface IPetConvertor
    {
        public Pet PetEntityConvertor(PetEntity petEntity);


        public PetEntity PetConvert(Pet pet);
    }
}