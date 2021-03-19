using AnekProvider.Core.Parsers;
using AnekProvider.DataModels.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public class BDotRuAnek : ParsableAnek
    {
        protected override IParser Parser { get; set; } = new BDotRuAnekParser();
    }
}
