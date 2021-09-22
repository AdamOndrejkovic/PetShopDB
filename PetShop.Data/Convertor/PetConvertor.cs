using PetShop.Core.Models;
using PetShop.Datas.Entities;

namespace PetShop.Datas.Convertor
{
    public class PetConvertor
    {
        public Pet PetEntityConvertor(PetEntity petEntity)
        {
            return new Pet()
            {
                Id = petEntity.Id,
                Name = petEntity.Name,
                Birthdate = petEntity.Birthdate,
                SoldDate = petEntity.SoldDate,
                Type = new PetType()
                {
                    Id = petEntity.Type.Id,
                    Name = petEntity.Type.Type,
                },
                Color = new PetColor()
                {
                    Id = petEntity.Color.Id,
                    Color = petEntity.Color.Color
                },
                Price = petEntity.Price,
                Owner = new Owner()
                {
                    Id = petEntity.Owner.Id
                    Id = petEntity.Owner.Id
                    Id = petEntity.Owner.Id
                    Id = petEntity.Owner.Id
                    Id = petEntity.Owner.Id
                    Id = petEntity.Owner.Id
                }
                
            };
        }

        public PetEntity PetConvert(Pet pet)
        {
            return new PetEntity();
        }
    }
}