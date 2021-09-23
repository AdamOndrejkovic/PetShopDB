using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IMessageService
    {
        List<Message> GetAllMessages();
        Message GetMessageById(long id);
        Message UpdateMessage(long id, Message message);
        Message CreateMessage(Message message);
        Message DeleteMessage(long id);
    }
}