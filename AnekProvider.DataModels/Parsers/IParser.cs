using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.Core.Parsers
{
    public interface IParser
    {
        public string GetTitle(string url);
        public string GetText(string url);
    }
}
