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
            int parsersAmount = 1; // добавить, когда появятся новые парсеры
            Random r = new Random();
            switch (r.Next(parsersAmount))
            {
                case 0:
                    {
                        BDotSiteAnekParser parser = new BDotSiteAnekParser();
                        string redirUrl = "https://baneks.site/" + GetRedirUrl("https://baneks.site/random");
                        return new BDotSiteAnek() { Title = parser.GetTitle(redirUrl), Uri = redirUrl };
                    }
            }
            return null;

        }

        private static string GetRedirUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string redirUrl = response.Headers["Location"];
            response.Close();
            return redirUrl;
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
            anek.User = user.ID;
            anek = CreateAnek(anek);                
        }

        public static List<BaseAnek> GetAneks(string userProfileID)
        {
            var users = userService.Get(el => el.UserProfile == userProfileID);
            if (!users.Any())
                return new List<BaseAnek>();
            var user = users.First();
            return anekService.Get(an => an.User == user.ID).ToList();
        }

        public static BaseAnek GetAnek(Guid guid)
        {
            return anekService.FindByID(guid);
        }
            
    }
}
