using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string UserProfile { get; set; }
        public string UserName { get; set; }
        public List<BaseAnek> Aneks { get; set; } = new List<BaseAnek>();
    }
}
