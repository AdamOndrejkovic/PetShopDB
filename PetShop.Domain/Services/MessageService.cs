using System.Collections.Generic;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        
        
        public List<Message> GetAllMessages()
        {
            return _messageRepository.GetAllMessages();
        }

        public Message GetMessageById(long id)
        {
            return _messageRepository.GetMessageById(id);
        }

        public Message UpdateMessage(long id, Message message)
        {
            return _messageRepository.UpdateMessage(id, message);
        }

        public Message CreateMessage(Message message)
        {
            return _messageRepository.CreateMessage(message);
        }

        public Message DeleteMessage(long id)
        {
            return _messageRepository.DeleteMessage(id);
        }
    }
}