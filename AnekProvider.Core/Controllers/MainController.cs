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

        public static Anek GetRandomAnek()
        {
            BAnekParser parser = new BAnekParser();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://baneks.site/random");
            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string redirUrl = "https://baneks.site/" + response.Headers["Location"];
            response.Close();
            return parser.GetAnek(redirUrl);
        }

        public static User CreateUser(string profileID)
        {
            var users = userService.Get(el => el.UserProfile == profileID);
            if (users.Any())
                return users.First();
            else
                return userService.Create(profileID);
        }

        public static Anek CreateAnek(string url, string text, string title)
        {
            var aneks = anekService.Get(el => el.Uri == url);
            if (aneks.Any())  
                return aneks.First();
            else
            {
                var anek = anekService.Create(url, text, title);
                return anek;
            }                
        }

        public static void SaveAnek(string userProfileID, string anekLink)
        {
            BAnekParser parser = new BAnekParser();
            Anek anek = parser.GetAnek(anekLink);
            User user = CreateUser(userProfileID);
            anek = CreateAnek(anek.Uri, anek.Text, anek.Title);
            if (!user.Aneks.Where(el => el.ID == anek.ID).Any())
            {
                user.Aneks.Add(anek);
                userService.Update(user);
            }
        }
        public static List<Anek> GetAneks(string userProfileID)
        {
            var users = userService.Get(el => el.UserProfile == userProfileID);
            if (!users.Any())
                return new List<Anek>();
            var anekIDs = users.First().Aneks.Select(el => el.ID).AsQueryable();
            return anekIDs.Select(el => anekService.FindByID(el)).ToList();
        }

        public static Anek GetAnek(Guid guid)
        {
            return anekService.FindByID(guid);
        }
            
    }
}
