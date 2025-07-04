﻿using EasyConnect.Models.Entities.Base;

namespace EasyConnect.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        // Navigation property
        public ICollection<BusinessProfile> Businesses { get; set; } = new List<BusinessProfile>();

    }
}
