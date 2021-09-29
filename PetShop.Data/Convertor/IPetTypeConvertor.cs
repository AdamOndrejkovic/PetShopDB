using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas.Convertor
{
    public interface IPetTypeConvertor
    {
        public PetEntity ConvertToPetTypeEntity(PetType petType);

        public PetType ConvertToPetType(PetTypeEntity petEntity);
    }
}