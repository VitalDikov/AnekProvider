using AnekProvider.DataModels.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;

namespace AnekProvider.Core.Parsers
{
    public class BAnekParser : IParser<Anek>
    {
        public Anek GetAnek(string url)
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url);
            }

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(htmlCode);
            string ClassToGet = "block-content mdl-card__supporting-text mdl-color--grey-300 mdl-color-text--grey-900";
            var trNode = html.DocumentNode.SelectSingleNode($"//div[contains(@class, '{ClassToGet}')]");

            foreach (HtmlNode node in trNode.SelectNodes("//br"))
                node.ParentNode.ReplaceChild(html.CreateTextNode("\n"), node);

            return new Anek() { Text = trNode.InnerText, Uri = new Uri(url)};
        }
    }
}
