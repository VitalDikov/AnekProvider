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
                "/анек - Осмотреть случайный анек\n" +
                "/лайк - Ответьте этой командой на анек, чтобы добавить его в любимые \n" +
                "/все - Открыть список сохраненных анеков\n" +
                "/покеж - Ответьте этой командой на название анека из списка любимых, чтобы получить полную версию \n" +
                "/новый - Чтобы сохранить свой анек, ответьте на сообщения формата <Заголовок>~~~<Текст> с командой /новый\n" +
                "/help - Открыть это меню\n";
            return Out;
        }

        void Save(User user, BaseAnek anek);
        List<BaseAnek> GetAneks(string userProfileID);
        BaseAnek GetAnek(Guid anekGuid);
    }
}
