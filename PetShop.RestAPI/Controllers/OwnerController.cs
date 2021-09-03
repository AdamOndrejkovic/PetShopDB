using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.IServices;
using PetShop.Core.Models;

namespace PetShop.RestAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        
        //Create Owner
        [HttpPost]
        public ActionResult<Owner> CreateOwner([FromBody] Owner owner)
        {
            var createOwner = _ownerService.CreateOwner(owner);
            if (createOwner != null)
            {
                return createOwner;
            }

            return BadRequest("Owner Could not be created. Please Try again");
        }

        //Read Owner
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> GetOwners()
        {
            var owners = _ownerService.GetOwners();
            if (owners != null)
            {
                //Ask about
                return new ActionResult<IEnumerable<Owner>>(owners);
            }

            return BadRequest("No owners were found");
        }

        [HttpGet("{id}")]
        public ActionResult<Owner> GetOwnerById(int id)
        {
            var ownerById = _ownerService.GetOwnerById(id);
            if (ownerById != null)
            {
                return ownerById;
            }

            return BadRequest($"Owner with id {id} could not be found");
        }
        
        //Update Owner
        [HttpPut("{id}")]
        public ActionResult<Owner> UpdateOwner(int id, [FromBody] Owner ownerToUpdate)
        {
            var ownerUpdated = _ownerService.UpdateOwner(ownerToUpdate);
            if (ownerUpdated != null)
            {
                return ownerToUpdate;
            }

            return BadRequest($"Owner of id {ownerToUpdate.Id} could not be updated");
        }

        //Delete Owner
        [HttpDelete("{id}")]
        public ActionResult<Owner> DeleteOwner(int id)
        {
           var ownerDelete = _ownerService.DeleteOwner(id);
           if (ownerDelete != null)
           {
               return ownerDelete;
           }

           return BadRequest($"Owner with id {id} could not be deleted");
        }

    }
}