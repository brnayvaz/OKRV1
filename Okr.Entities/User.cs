﻿using Okr.Entities.Base;

namespace Okr.Entities
{
    public class User:BaseEntity
    {
        public string name { get; set; }
        public string password { get; set; }
        public string role { get; set; }

    }
}