using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.Filtering;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.RestAPI.Dto;
using BadRequestResult = Microsoft.AspNet.Mvc.BadRequestResult;

namespace PetShop.RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PetReadAllDto>> Get([FromQuery] Filter filter)
        {
            try
            {
                var totalCount = _petService.GetTotalPetCount();
                return Ok(_petService.GetPets(filter).Select(p => new PetReadAllDto {Id = p.Id, Name = p.Name, Price = p.Price}));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<FilteredPetDto> Get(int id)
        {
            var filteredPet = _petService.FindPetById(id);
            if (filteredPet != null)
            {
                return new FilteredPetDto() {PetName = filteredPet.Name, OwnerName = filteredPet.Owner.FirstName};
            }

            return BadRequest($"No pet was found with id: {id}. Try to use other id.");
        }

        [HttpPost]
        public ActionResult<PetCreatedDto> Post([FromBody] PetCreateDto petValue)
        {
            if (string.IsNullOrEmpty(petValue.Name))
            {
                return BadRequest("First name required");
            }

            var createdPet =_petService.CreatePet(new Pet(){Name = petValue.Name, Owner = new Owner(){Id = petValue.OwnerId}, Type = new PetType(){Id = petValue.PetTypeId}});
            return new PetCreatedDto() {Name = createdPet.Name};
        }

        [HttpPut("{id}")]
        public ActionResult<UpdatePetDto> Put(int id, [FromBody] Pet pet)
        {
            if (id < 1 || id != pet.Id)
            {
                return BadRequest("Parameter Id and customer Id must be the same");
            }

            return new UpdatePetDto() {Name = _petService.UpdatePet(pet).Name};
        }

        [HttpDelete("{id}")]
        public ActionResult<DeletePetDto> DeletePet(int id)
        {
            var deletedPet = _petService.DeletePet(id);

            if (deletedPet == null)
            {
                return BadRequest("An error occurred. Your pet couldn't be deleted");
            }

            return new DeletePetDto(){Name = deletedPet.Name};
        }
    }
}