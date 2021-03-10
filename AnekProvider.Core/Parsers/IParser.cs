using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.Core.Parsers
{
    public interface IParser<TAnek> where TAnek: class
    {
        public TAnek GetAnek(string url);
    }
}
