﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Entities
{
    public class User
    {
        public Guid ID { get; set; }
        public string UserProfile { get; set; }
        public List<Anek> Aneks { get; set; } = new List<Anek>();
    }
}
