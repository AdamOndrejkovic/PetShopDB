using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Domain.IRepositories
{
    public interface IMessageRepository
    {
        List<Message> GetAllMessages();
        Message GetMessageById(long id);
        Message UpdateMessage(long id, Message message);
        Message CreateMessage(Message message);
        Message DeleteMessage(long id);
    }
}