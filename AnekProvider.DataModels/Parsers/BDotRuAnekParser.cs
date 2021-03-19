using AnekProvider.Core.Parsers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AnekProvider.DataModels.Parsers
{
    public class BDotRuAnekParser : IParser
    {
        public string GetText(string url)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(htmlCode);
            var textNode = html.DocumentNode.SelectSingleNode($"//article//p");
            return textNode.InnerText;
        }

        public string GetTitle(string url)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(htmlCode);
            string title = html.DocumentNode.SelectSingleNode($"//title").InnerText.Split('|')[0].Trim();

            return title;
        }
    }
}
