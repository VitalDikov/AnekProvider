using Anekprovider.VkClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Utils;

namespace Anekprovider.VkClient.Controllers
{
    [Route("api/callback")]
    [ApiController]
    public class MessageHandler : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;
        private readonly MessageController _controller;
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        public MessageHandler(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
            _controller = new MessageController(vkApi);
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
                        string firstword = msg.Text.Split()[0];
                        string text = msg.Text.ToString();
                        if (firstword.Contains('@'))
                            if (!firstword.Contains("anekprovider"))
                                return Ok("ok"); 
                            else
                                msg.Text = msg.Text.Remove(0, firstword.Length).Remove(0, 1);

                        switch (msg.Text)
                        {
                            case "/анек":
                            case "Хочу анекдот":
                                _controller.RandomAnek(msg);
                                break;
                            case "/лайк":
                                _controller.Save(msg);
                                break;
                            case "/все":
                            case "Мои любимые анеки":
                                _controller.All(msg);
                                break;
                            case "/покеж":
                                _controller.ShowAnek(msg);
                                break;
                            case "/новый":
                                _controller.CreateUserAnek(msg);
                                break;
                        }
                        break;
                    }
            }
            return Ok("ok");
        }
    }
}
