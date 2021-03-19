using AnekProvider.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AnekProvider.DataModels.Entities
{
    public class BDotSiteAnek : ParsableAnek
    {
        [JsonIgnore]
        protected override IParser Parser { get; set; } = new BDotSiteAnekParser();
    }
}
