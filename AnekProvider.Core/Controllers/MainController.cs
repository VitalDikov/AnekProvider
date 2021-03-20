using AnekProvider.Core.Models;
using AnekProvider.DataModels.Entities;
using AnekProvider.DataModels.Repositories;
using AnekProvider.DataModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnekProvider.Core.Controllers
{
    public class MainController
    {
        private AnekService anekService;
        private UserService userService;

        public MainController()
        {
            AnekContext db = new AnekContext();
            UnitOfWork uof = new UnitOfWork(db);

            anekService = new AnekService(uof);
            userService = new UserService(uof);
        }

        public ParsableAnek GetRandomAnek()
        {
            return AnekRandomizer.GetRandomAnek();
        }

        public User CreateUser(User user)
        {
            var users = userService.Get(el => el.UserProfile == user.UserProfile);
            if (users.Any())
                return users.First();
            else
                return userService.Create(user);
        }

        public BaseAnek CreateAnek(BaseAnek anek)
        {
            return anekService.Create(anek);                
        }

        public void SaveAnek(User user, BaseAnek anek)
        {
            user = CreateUser(user);
            anek.User = user.ID;
            anek = CreateAnek(anek);                
        }

        public List<BaseAnek> GetAneks(string userProfileID)
        {
            var users = userService.Get(el => el.UserProfile == userProfileID);
            if (!users.Any())
                return new List<BaseAnek>();
            var user = users.First();
            return anekService.Get(an => an.User == user.ID).ToList();
        }

        public BaseAnek GetAnek(Guid guid)
        {
            return anekService.FindByID(guid);
        }
            
    }
}
