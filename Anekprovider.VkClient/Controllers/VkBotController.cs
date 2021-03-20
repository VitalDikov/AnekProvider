using AnekProvider.Core.BotClinets;
using AnekProvider.Core.Controllers;
using AnekProvider.DataModels.Entities;
using System;
using System.Collections.Generic;

namespace Anekprovider.VkClient.Controllers
{
    public class VkBotController : IBotController
    {
        private MainController _mainController;
        public VkBotController()
        {
            _mainController = new MainController();
        }


        public ParsableAnek GetRandomAnek()
        {
            return _mainController.GetRandomAnek();
        }
        public void Save(User user, BaseAnek anek)
        {
            _mainController.SaveAnek(user, anek);
        }
        public List<BaseAnek> GetAneks(string userProfileID)
        {
            return _mainController.GetAneks(userProfileID);
        }
        public BaseAnek GetAnek(Guid anekGuid)
        {
            return _mainController.GetAnek(anekGuid);
        }
    }
}
