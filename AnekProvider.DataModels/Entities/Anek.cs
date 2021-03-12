using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public class Anek
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; } 
        public string Text { get; set; }
    }
}
