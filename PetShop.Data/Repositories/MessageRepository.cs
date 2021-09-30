using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly PetShopContext _context;

        public MessageRepository(PetShopContext context)
        {
            _context = context;
        }
        
        public Message Get(long id)
        {
            return _context.Messages.FirstOrDefault(message => message.Id == id);
        }
        

        public List<Message> GetAllMessages()
        {
            return _context.Messages.ToList();
        }

        public Message GetMessageById(long id)
        {
            return _context.Messages.FirstOrDefault(m => m.Id == id);
        }

        public Message UpdateMessage(long id, Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            _context.SaveChanges();
            return message;
        }
        public Message CreateMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public Message DeleteMessage(long id)
        {
            var message = _context.Messages.FirstOrDefault(message => message.Id == id);
            if (message == null) return null;
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return message;
        }
    }
}
