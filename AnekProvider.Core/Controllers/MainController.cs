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
        private readonly AnekService _anekService;
        private readonly UserService _userService;

        public MainController()
        {
            AnekContext db = new AnekContext();
            UnitOfWork uof = new UnitOfWork(db);

            _anekService = new AnekService(uof);
            _userService = new UserService(uof);
        }

        public ParsableAnek GetRandomAnek()
        {
            return AnekRandomizer.GetRandomAnek();
        }

        public User CreateUser(User user)
        {
            var users = _userService.Get(el => el.UserProfile == user.UserProfile);
            if (users.Any())
                return users.First();
            else
                return _userService.Create(user);
        }

        public BaseAnek CreateAnek(BaseAnek anek)
        {
            return _anekService.Create(anek);                
        }

        public void SaveAnek(User user, BaseAnek anek)
        {
            user = CreateUser(user);
            anek.User = user.ID;
            anek = CreateAnek(anek);                
        }

        public List<BaseAnek> GetAneks(string userProfileID)
        {
            var users = _userService.Get(el => el.UserProfile == userProfileID);
            if (!users.Any())
                return new List<BaseAnek>();
            var user = users.First();
            return _anekService.Get(an => an.User == user.ID).ToList();
        }

        public BaseAnek GetAnek(Guid guid)
        {
            return _anekService.FindByID(guid);
        }
            
    }
}
