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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Anekprovider.VkClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;
        private readonly IBotController _controller;
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
                                RandomAnek(msg);
                                break;
                            case "/лайк":
                                Save(msg);
                                break;
                            default:
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
            Anek anek = _controller.GetRandomAnek();
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = anek.Text,
                Payload = JsonSerializer.Serialize(anek)
            });
        }

        private void Help(Message msg)
        {
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = _controller.Help()
            });
        }
        private void Save(Message msg)
        {
            if(msg.ReplyMessage == null || String.IsNullOrEmpty(msg.ReplyMessage.Payload))
            {
                _vkApi.Messages.Send(new MessagesSendParams
                {
                    RandomId = new DateTime().Millisecond,
                    PeerId = msg.PeerId.Value,
                    Message = "Перешлите анек, пожалуйста"
                });
                return;
            }
            _controller.Save(msg.UserId.ToString(), JsonSerializer.Deserialize<Anek>(msg.ReplyMessage.Payload).Uri);
        }

    }
}
