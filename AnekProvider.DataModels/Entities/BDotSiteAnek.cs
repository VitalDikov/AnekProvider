using AnekProvider.Core.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public class BDotSiteAnek : ParsableAnek
    {
        protected override IParser Parser { get; set; } = new BDotSiteAnekParser();
    }
}
