using System.Collections.Generic;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Datas.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public List<Message> GetAllMessages()
        {
            throw new System.NotImplementedException();
        }

        public Message GetMessageById(long id)
        {
            throw new System.NotImplementedException();
        }

        public Message UpdateMessage(long id, Message message)
        {
            throw new System.NotImplementedException();
        }

        public Message CreateMessage(Message message)
        {
            throw new System.NotImplementedException();
        }

        public Message DeleteMessage(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}