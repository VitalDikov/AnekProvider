using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AnekProvider.DataModels.Entities
{
    public class CustomAnek : BaseAnek
    {
        [JsonIgnore]
        public string Text { get; set; }

        public override string GetText()
        {
            return this.Text;
        }
    }
}
