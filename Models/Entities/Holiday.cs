using EasyConnect.Models.Entities.Base;

namespace EasyConnect.Models.Entities
{
    public class Holiday : BaseEntity
    {
        public int BusinessProfileId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }

        public DateTime Date { get; set; } // Tek gün tatil (örn: 2025-05-01)

        public string? Description { get; set; } // (örn: "1 Mayıs Emek Bayramı")
    }
}
