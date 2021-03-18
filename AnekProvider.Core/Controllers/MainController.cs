using AnekProvider.Core.Parsers;
using AnekProvider.DataModels.Entities;
using AnekProvider.DataModels.Repositories;
using AnekProvider.DataModels.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AnekProvider.Core.Controllers
{
    public static class MainController
    {
        private static AnekService anekService;
        private static UserService userService;

        static MainController()
        {
            AnekContext db = new AnekContext();
            UnitOfWork uof = new UnitOfWork(db);

            anekService = new AnekService(uof);
            userService = new UserService(uof);
        }

        public static ParsableAnek GetRandomAnek()
        {
            BAnekParser parser = new BAnekParser();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://baneks.site/random");
            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string redirUrl = "https://baneks.site/" + response.Headers["Location"];
            response.Close();
            return new ParsableAnek() { Title = parser.GetTitle(redirUrl), Uri = redirUrl };
        }

        public static User CreateUser(User user)
        {
            var users = userService.Get(el => el.UserProfile == user.UserProfile);
            if (users.Any())
                return users.First();
            else
                return userService.Create(user);
        }

        public static BaseAnek CreateAnek(BaseAnek anek)
        {
            return anekService.Create(anek);                
        }

        public static void SaveAnek(User user, BaseAnek anek)
        {
            user = CreateUser(user);
            anek = CreateAnek(anek);
            if (!user.Aneks.Where(el => el.ID == anek.ID).Any())
            {
                user.Aneks.Add(anek);
                userService.Update(user);
            }
        }

        public static List<BaseAnek> GetAneks(string userProfileID)
        {
            var users = userService.Get(el => el.UserProfile == userProfileID);
            if (!users.Any())
                return new List<BaseAnek>();
            var anekIDs = users.First().Aneks.Select(el => el.ID).AsQueryable();
            return anekIDs.Select(el => anekService.FindByID(el)).ToList();
        }

        public static BaseAnek GetAnek(Guid guid)
        {
            return anekService.FindByID(guid);
        }
            
    }
}
