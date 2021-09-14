using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetShop.Core.IServices;
using PetShop.Core.Models;

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
        public IEnumerable<Pet> Get()
        {
            return _petService.GetPets();
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            var filteredPet = _petService.FindPetById(id);
            if (filteredPet != null)
            {
                return filteredPet;
            }

            return BadRequest($"No pet was found with id: {id}. Try to use other id.");
        }

        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet petValue)
        {
            if (string.IsNullOrEmpty(petValue.Name))
            {
                return BadRequest("First name required");
            }

            return _petService.CreatePet(petValue);
        }

        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            if (id < 1 || id != pet.Id)
            {
                return BadRequest("Parameter Id and customer Id must be the same");
            }

            return _petService.UpdatePet(pet);
        }

        [HttpDelete("{id}")]
        public ActionResult<Pet> DeletePet(int id)
        {
            var deletedPet = _petService.DeletePet(id);

            if (deletedPet == null)
            {
                return BadRequest("An error occurred. Your pet couldn't be deleted");
            }

            return deletedPet;
        }
    }
}