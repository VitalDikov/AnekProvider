using AnekProvider.Core.Parsers;
using AnekProvider.DataModels.Entities;
using AnekProvider.DataModels.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AnekProvider.Core.Models
{
    public static class AnekRandomizer
    {
        public static ParsableAnek GetRandomAnek()
        {
            int parsersAmount = 2; // добавить, когда появятся новые парсеры
            Random r = new Random();
            switch (r.Next(parsersAmount))
            {
                case 0:
                    {
                        BDotSiteAnekParser parser = new BDotSiteAnekParser();
                        string redirUrl = "https://baneks.site/" + GetRedirUrl("https://baneks.site/random");
                        return new BDotSiteAnek() { Title = parser.GetTitle(redirUrl), Uri = redirUrl };
                    }
                case 1:
                    {
                        BDotRuAnekParser parser = new BDotRuAnekParser();
                        string redirUrl = GetRedirUrl("https://baneks.ru/random");
                        var titlewords = parser.GetTitle(redirUrl).Split();
                        string title = String.Join(' ', titlewords.Take(Math.Min(10, titlewords.Count())));
                        return new BDotRuAnek() { Title = title, Uri = redirUrl };
                    }
                default:
                    throw new Exception("Invalid Parsers Amount");
            }


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
    }
}
