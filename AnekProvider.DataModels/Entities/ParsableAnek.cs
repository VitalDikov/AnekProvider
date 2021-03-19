using AnekProvider.Core.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AnekProvider.DataModels.Entities
{
    public abstract class ParsableAnek : BaseAnek
    {
        public string Uri { get; set; }
        [NotMapped]
        protected abstract IParser Parser { get; set; }
        public override string GetText()
        {
            return Parser.GetText(this.Uri);
        }
    }
}
