using Anekprovider.VkClient.Models;
using AnekProvider.Core.BotClinets;
using System.Collections.Generic;
using System.Linq;
using VkNet.Abstractions;
using VkNet.Model;

namespace Anekprovider.VkClient.Controllers
{
    public class MessageController
    {
        private readonly IVkApi _vkApi;
        private readonly IBotController _controller;
        private readonly AnekManager _anekManager;
        private readonly MessageSender _sender;
        public MessageController(IVkApi vkApi)
        {
            _vkApi = vkApi;
            _controller = new VkBotController();
            _anekManager = new AnekManager(_controller);
            _sender = new MessageSender(_vkApi);
        }

        public void All(Message msg)
        {
            _sender.SendPack(_anekManager.All(msg));
        }

        public void CreateUserAnek(Message msg)
        {
            string nickname = _vkApi.Users.Get(new List<long>() { (long)msg.FromId }).First().LastName;
            _sender.Send(_anekManager.CreateUserAnek(msg, nickname));
        }
        public void Help(Message msg)
        {
            _sender.Send(_anekManager.Help(msg));
        }
        public void RandomAnek(Message msg)
        {
            _sender.Send(_anekManager.RandomAnek(msg));
        }
        public void Save(Message msg)
        {
            string nickname = _vkApi.Users.Get(new List<long>() { (long)msg.FromId }).First().LastName;
            _sender.Send(_anekManager.Save(msg, nickname));
        }
        public void ShowAnek(Message msg)
        {
            _sender.Send(_anekManager.ShowAnek(msg));
        }
    }
}
