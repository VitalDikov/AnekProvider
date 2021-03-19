using AnekProvider.Core.Parsers;
using AnekProvider.DataModels.Parsers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AnekProvider.DataModels.Entities
{
    public class BDotRuAnek : ParsableAnek
    {
        [JsonIgnore]
        protected override IParser Parser { get; set; } = new BDotRuAnekParser();
    }
}
