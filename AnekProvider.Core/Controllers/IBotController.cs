using AnekProvider.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.Core.BotClinets
{
    public interface IBotController
    {
        Anek GetRandomAnek();
        string Help()
        {
            string Out = "Список команд:\n" +
                "/help - открыть это меню\n" +
                "/анек - посмотреть случайный анек\n" +
                "/лайк - перешлите понравившийся анек и сохраните его";
            return Out;
        }

        void Save(string userProfileID, string link);
    }
}
