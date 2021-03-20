using Anekprovider.VkClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Utils;

namespace Anekprovider.VkClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageHandlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;
        private readonly MessageController _controller;
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        public MessageHandlerController(IVkApi vkApi, IConfiguration configuration)
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
                            default:
                                if (msg.Text.Contains("/новый"))
                                    _controller.CreateUserAnek(msg);
                                else
                                    _controller.Help(msg);
                                break;
                        }
                        break;
                    }
            }
            return Ok("ok");
        }
    }
}
