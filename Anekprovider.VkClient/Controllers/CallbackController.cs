using Anekprovider.VkClient.Models;
using AnekProvider.Core.BotClinets;
using AnekProvider.DataModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using Newtonsoft.Json;
using VkNet.Model.Keyboard;
using VkNet.Enums.SafetyEnums;

namespace Anekprovider.VkClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;
        private readonly IBotController _controller;

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
            _controller = new VkBotController();
        }

        [HttpPost]
        public IActionResult Callback([FromBody] UpdatesDto updates)
        {
            switch (updates.Type)
            {
                case "confirmation":
                    return Ok(_configuration["Config:Confirmation"]);

                case "message_new":
                    {
                        var msg = Message.FromJson(new VkResponse(updates.Object));

                        switch (msg.Text)
                        {
                            case "/анек":
                            case "Хочу анекдот":
                                RandomAnek(msg);
                                break;
                            case "/лайк":
                                Save(msg);
                                break;
                            case "/все":
                            case "Мои любимые анеки":
                                All(msg);
                                break;
                            case "/покеж":
                                ShowAnek(msg);
                                break;
                            default:
                                if (msg.Text.Contains("/новый"))
                                    CreateUserAnek(msg);
                                else
                                    Help(msg);
                                break;
                        }
                        break;
                    }
            }
            return Ok("ok");
        }

        private void RandomAnek(Message msg)
        {
            ParsableAnek anek = _controller.GetRandomAnek();
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = anek.GetText(),
                Payload = JsonConvert.SerializeObject(anek, _settings)
            });
        }
        private void CreateUserAnek(Message msg)
        {
            var args = msg.ReplyMessage.Text.Split("~~~");
            string title = args[0].Trim();
            string text = args[1].Trim();

            CustomAnek anek = new CustomAnek() { Text = text, Title = title };

            string nickname = _vkApi.Users.Get(new List<long>() { (long)msg.FromId }).First().LastName;
            var user = new AnekProvider.DataModels.Entities.User() { UserProfile = msg.FromId.ToString(), UserName = nickname };
            _controller.Save(user, anek);
        }

        private void Help(Message msg)
        {
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = _controller.Help(),
                Keyboard = new KeyboardBuilder()
                        .AddButton("Хочу анекдот", "", KeyboardButtonColor.Positive)
                        .AddButton("Мои любимые анеки", "", KeyboardButtonColor.Negative)
                        .Build()
            }) ;
        }
        private void Save(Message msg)
        {
            string nickname = _vkApi.Users.Get(new List<long>() { (long)msg.FromId }).First().LastName;
            var user = new AnekProvider.DataModels.Entities.User() { UserProfile = msg.FromId.ToString(), UserName = nickname };

            string payload = msg.ReplyMessage != null ?
                msg.ReplyMessage.Payload :
                msg.ForwardedMessages.First().ForwardedMessages.First().Payload;

            var anek = (BaseAnek)JsonConvert.DeserializeObject(payload, _settings);
            anek.ID = Guid.Empty;
            anek.User = Guid.Empty;
            _controller.Save(user, anek);
        }
        private void All(Message msg)
        {
            var aneks = _controller.GetAneks(msg.FromId.ToString());
            if(!aneks.Any())
                _vkApi.Messages.Send(new MessagesSendParams
                {
                    RandomId = new DateTime().Millisecond,
                    PeerId = msg.PeerId.Value,
                    Message = "Вы еще не сохранили ни одного анека("
                });
            else
            {
                foreach (var anek in aneks)
                {
                    _vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = msg.PeerId.Value,
                        Message = $"{anek.Title}",
                        Payload = JsonConvert.SerializeObject(anek, _settings)
                    });
                }
            }
        }
        
        private void ShowAnek(Message msg)
        {
            BaseAnek anek = _controller.GetAnek(((BaseAnek)JsonConvert.DeserializeObject(msg.ReplyMessage.Payload, _settings)).ID);
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = $"{anek.GetText()}",
                Payload = msg.ReplyMessage.Payload
            });
        }

    }
}
