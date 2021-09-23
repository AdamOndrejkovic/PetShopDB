using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using ActionResult = Microsoft.AspNet.Mvc.ActionResult;

namespace PetShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
            
        }

        // GET: api/Message
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Message>> GetMessages()
        {
            return _messageService.GetAllMessages();
        }

        // GET: api/Message/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Message> GetMessage(long id)
        {
            var message = _messageService.GetMessageById(id);
            
            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Message/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult<Message> PutMessage(long id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            return _messageService.UpdateMessage(id, message);
        }

        // POST: api/Message
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Message> PostMessage(Message message)
        {
            
            

            return _messageService.CreateMessage(message);
        }

        // DELETE: api/Message/5
        [HttpDelete("{id}")]
        public ActionResult<Message> DeleteMessage(long id)
        {
            return _messageService.DeleteMessage(id);
        }
    }
}