using AnekProvider.Core.BotClinets;
using AnekProvider.Core.Controllers;
using AnekProvider.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anekprovider.VkClient.Controllers
{
    public class VkBotController : IBotController
    {
        public Anek GetRandomAnek() =>
            MainController.GetRandomAnek();

        public void Save(string userProfileID, string link)
        {
            MainController.SaveAnek(userProfileID, link);
        }

        public List<Anek> GetAneks(string userProfileID)
        {
            return MainController.GetAneks(userProfileID);
        }
        public Anek GetAnek(Guid anekGuid)
        {
            return MainController.GetAnek(anekGuid);
        }

        public List<string> GetComments(string url)
        {
            return MainController.GetAnekComments(url);
        }

    }
}
