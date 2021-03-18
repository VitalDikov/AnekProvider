using AnekProvider.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AnekProvider.DataModels.Entities
{
    public class ParsableAnek : BaseAnek
    {
        public string Uri { get; set; }

        public override string GetText()
        {
            BAnekParser parser = new BAnekParser();
            return parser.GetText(this.Uri);
        }
    }
}
