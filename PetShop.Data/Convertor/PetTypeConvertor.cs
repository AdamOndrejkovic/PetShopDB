using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas.Convertor
{
    public class PetTypeConvertor
    {
        public PetEntity ConvertToPetTypeEntity(PetType petType)
        {
            return new PetEntity();
        }

        public PetType ConvertToPetType(PetEntity petEntity)
        {
            return new PetType();
        }
    }
}