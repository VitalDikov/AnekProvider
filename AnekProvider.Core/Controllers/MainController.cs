using AnekProvider.Core.Parsers;
using AnekProvider.DataModels.Entities;
using AnekProvider.DataModels.Repositories;
using AnekProvider.DataModels.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnekProvider.Core.Controllers
{
    public class MainController
    {
        private AnekService anekService;
        private UserService userService;

        public MainController()
        {
            var options = new DbContextOptionsBuilder<AnekContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

            AnekContext db = new AnekContext(options);
            UnitOfWork uof = new UnitOfWork(db);

            anekService = new AnekService(uof);
            userService = new UserService(uof);
        }

        public Anek GetRandomAnek()
        {
            BAnekParser parser = new BAnekParser();
            return parser.GetAnek("https://baneks.site/random");
        }

        public User CreateUser(string profileID)
        {
            var users = userService.Get(el => el.UserProfileID == profileID);
            if (users.Any())
                return users.First();
            else
                return userService.Create(profileID);
        }

        public Anek CreateAnek(string url, string text)
        {
            var aneks = anekService.Get(el => el.Uri == url);
            if (aneks.Any())  
                return aneks.First();
            else
            {
                var anek = anekService.Create(url, text);
                return anek;
            }                
        }

        public Anek SaveAnek(User user, Anek anek) // не забудь, что после добавления в бд у него обновляется Guid
        {
            anek = CreateAnek(anek.Uri, anek.Text);
            if (!user.Aneks.Where(el => el.ID == anek.ID).Any())
            {
                user.Aneks.Add(anek);
                userService.Update(user);
            }
            return anek;
        }
        public List<Anek> GetAneks(User user)
        {
            var anekIDs = user.Aneks.Select(el => el.ID).AsQueryable();
            return anekIDs.Select(el => anekService.FindByID(el)).ToList();
        }
            
    }
}
