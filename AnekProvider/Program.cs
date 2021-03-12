using AnekProvider.Core.Controllers;
using AnekProvider.Core.Parsers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnekProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            MainController controller = new MainController();
            var user = controller.CreateUser("https://vk.com/id_vitosek");
            var anek = controller.GetRandomAnek();
            anek = controller.SaveAnek(user, anek);
            var aneks = controller.GetAneks(user);
            Console.WriteLine(aneks.First().Text);
        }
    }
}
