using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public class VkUser
    {
        public Guid ID { get; set; }
        public string UserPage { get; set; }
        public List<Anek> Aneks { get; set; }
    }
}
