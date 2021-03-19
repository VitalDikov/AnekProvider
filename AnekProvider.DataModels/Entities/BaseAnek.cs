using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public abstract class BaseAnek
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public Guid User{ get; set; }
        public abstract string GetText();
    }
}
