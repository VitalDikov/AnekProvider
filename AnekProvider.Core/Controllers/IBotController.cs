using AnekProvider.DataModels.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.Core.BotClinets
{
    public interface IBotController
    {
        ParsableAnek GetRandomAnek();
        string Help()
        {
            string Out = "Список команд:\n" +
                "/help - открыть это меню\n" +
                "/анек - посмотреть случайный анек\n" +
                "/лайк - перешлите понравившийся анек и сохраните его\n" +
                "/все - открыть список сохраненных анеков\n" + 
                "/покеж - показать полный анек из списка";
            return Out;
        }

        void Save(User user, BaseAnek anek);
        List<BaseAnek> GetAneks(string userProfileID);
        BaseAnek GetAnek(Guid anekGuid);
    }
}
