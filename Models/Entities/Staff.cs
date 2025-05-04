using EasyConnect.Models.Entities.Base;

namespace EasyConnect.Models.Entities
{
    public class Staff : BaseEntity
    {
        public string FullName { get; set; }
        public int BusinessProfileId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
    }
}
