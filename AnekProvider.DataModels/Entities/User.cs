using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string UserProfileID { get; set; }
        public List<Anek> Aneks { get; set; }
    }
}
