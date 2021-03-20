using AnekProvider.Core.BotClinets;
using AnekProvider.DataModels.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace Anekprovider.VkClient.Models
{
    public class AnekManager
    {
        public IBotController _controller;
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
        public AnekManager(IBotController controller)
        {
            _controller = controller;
        }

        public MessagesSendParams RandomAnek(Message msg)
        {
            ParsableAnek anek = _controller.GetRandomAnek();
            return new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = anek.GetText(),
                Payload = JsonConvert.SerializeObject(anek, _settings)
            };
        }
        public MessagesSendParams CreateUserAnek(Message msg, string nickname)
        {
            if (msg.ReplyMessage.Text.Length > 990)
                return new MessagesSendParams
                {
                    RandomId = new DateTime().Millisecond,
                    PeerId = msg.PeerId.Value,
                    Message = "Слишком длинный анек("
                };

            var args = msg.ReplyMessage.Text.Split("~~~");
            string title = args[0].Trim();
            string text = args[1].Trim();

            CustomAnek anek = new CustomAnek() { Text = text, Title = title };

            var user = new AnekProvider.DataModels.Entities.User() { UserProfile = msg.FromId.ToString(), UserName = nickname };
            _controller.Save(user, anek);

            return new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = "Анек успешно добавлен!",
            };
        }

        public MessagesSendParams Help(Message msg)
        {
            return new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = _controller.Help(),
                Keyboard = new KeyboardBuilder()
                        .AddButton("Хочу анекдот", "", KeyboardButtonColor.Positive)
                        .AddButton("Мои любимые анеки", "", KeyboardButtonColor.Negative)
                        .Build()
            };
        }
        public MessagesSendParams Save(Message msg, string nickname)
        {
            var user = new AnekProvider.DataModels.Entities.User() { UserProfile = msg.FromId.ToString(), UserName = nickname };

            string payload = msg.ReplyMessage != null ?
                msg.ReplyMessage.Payload :
                msg.ForwardedMessages.First().ForwardedMessages.First().Payload;

            var anek = (BaseAnek)JsonConvert.DeserializeObject(payload, _settings);
            anek.ID = Guid.Empty;
            anek.User = Guid.Empty;
            _controller.Save(user, anek);

            return new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = "Анек успешно сохранён!",
            };
        }
        public List<MessagesSendParams> All(Message msg)
        {
            var aneks = _controller.GetAneks(msg.FromId.ToString());
            var Out = new List<MessagesSendParams>();
            if (!aneks.Any())
            {
                Out.Add(
                    new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = msg.PeerId.Value,
                        Message = "Вы еще не сохранили ни одного анека("
                    });
            }
            else
            {
                foreach (var anek in aneks)
                {
                    Out.Add(new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = msg.PeerId.Value,
                        Message = $"{anek.Title}",
                        Payload = JsonConvert.SerializeObject(anek, _settings)
                    });
                }
            }
            return Out;
        }

        public MessagesSendParams ShowAnek(Message msg)
        {
            BaseAnek anek = _controller.GetAnek(((BaseAnek)JsonConvert.DeserializeObject(msg.ReplyMessage.Payload, _settings)).ID);
            return new MessagesSendParams
            {
                RandomId = new DateTime().Millisecond,
                PeerId = msg.PeerId.Value,
                Message = $"{anek.GetText()}",
                Payload = msg.ReplyMessage.Payload
            };
        }
    }
}
