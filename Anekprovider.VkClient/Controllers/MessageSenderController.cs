using System.Collections.Generic;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;

namespace Anekprovider.VkClient.Controllers
{
    public class MessageSenderController
    {
        private IVkApi _vkApi;
        public MessageSenderController(IVkApi vkApi)
        {
            _vkApi = vkApi;
        }
        public void Send(MessagesSendParams msg)
        {
            _vkApi.Messages.Send(msg);
        }
        public void SendPack(List<MessagesSendParams> msgs)
        {
            msgs.ForEach(m => Send(m));
        }
    }
}
