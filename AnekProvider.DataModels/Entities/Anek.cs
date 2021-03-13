using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AnekProvider.DataModels.Entities
{
    public class Anek
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Uri { get; set; }
        [JsonIgnore]
        public string Text { get; set; }
    }
}
