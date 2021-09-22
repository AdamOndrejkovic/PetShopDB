using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.RestAPI.Dto;
using PetShop.RestAPI.Dto.PetType;

namespace PetShop.RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;

        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadPetTypeDto>> Get()
        {
            try
            {
                return Ok(_petTypeService.GetPetTypes()
                    .Select(pt => new ReadPetTypeDto() {Id = pt.Id, PetTypeName = pt.Name}));
            }
            catch (Exception e)
            {
                return BadRequest("Pet Types could not be loaded");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ReadPetTypeById> Get(int id)
        {
            var filteredPetType = _petTypeService.GetPetTypeById(id);
            if (filteredPetType != null)
            {
                return new ReadPetTypeById(){Id = filteredPetType.Id, PetTypeName = filteredPetType.Name};
            }

            return BadRequest($"No pet type was found with id: {id}. Try to use other id.");
        }

        [HttpPost]
        public ActionResult<CreatePetTypeDto> Post([FromBody] CreatePetTypeDto petTypeValue)
        {
            if (string.IsNullOrEmpty(petTypeValue.Name))
            {
                return BadRequest("First name required");
            }

            return new CreatePetTypeDto() {Name = _petTypeService.CreateNewPetType(petTypeValue.Name).Name};
        }

        [HttpPut("{id}")]
        public ActionResult<UpdatePetTypeDto> Put(int id, [FromBody] PetType petType)
        {
            if (id < 1 || id != petType.Id)
            {
                return BadRequest("Parameter Id and customer Id must be the same");
            }

            return new UpdatePetTypeDto() {Name = _petTypeService.UpdatePetType(id,petType.Name).Name};
        }

        [HttpDelete("{id}")]
        public ActionResult<DeletePetTypeDto> DeletePet(int id)
        {
            var deletedPet = _petTypeService.DeletePetType(id);

            if (deletedPet == null)
            {
                return BadRequest("An error occurred. Your pet couldn't be deleted");
            }

            return new DeletePetTypeDto(){Name = deletedPet.Name};
        }
    }
}