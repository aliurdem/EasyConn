using EasyConnect.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyConnect.Models.Entities
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }        
        public TimeSpan Duration { get; set; }     
        public decimal Price { get; set; }         

        public ICollection<BusinessProfile> BusinessProfiles { get; set; } = new List<BusinessProfile>();

    }
}
