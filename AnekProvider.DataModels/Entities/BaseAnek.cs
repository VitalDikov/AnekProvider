using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public abstract class BaseAnek
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public Guid UserID{ get; set; }
        public abstract string GetText();
    }
}
