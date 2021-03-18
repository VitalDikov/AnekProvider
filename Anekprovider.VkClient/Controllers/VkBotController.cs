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
        public ParsableAnek GetRandomAnek()
        {
            return MainController.GetRandomAnek();
        }
        public void Save(User user, BaseAnek anek)
        {
            MainController.SaveAnek(user, anek);
        }

        public List<BaseAnek> GetAneks(string userProfileID)
        {
            return MainController.GetAneks(userProfileID);
        }
        public BaseAnek GetAnek(Guid anekGuid)
        {
            return MainController.GetAnek(anekGuid);
        }
    }
}
