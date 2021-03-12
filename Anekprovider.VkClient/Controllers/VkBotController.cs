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
        

    }
}
