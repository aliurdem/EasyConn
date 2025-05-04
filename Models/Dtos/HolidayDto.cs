using EasyConnect.Models.Dtos.Base;
using EasyConnect.Models.Entities;

namespace EasyConnect.Models.Dtos
{
    public class HolidayDto : BaseDto
    {
        public int BusinessProfileId { get; set; }
        public DateTime Date { get; set; } // Tek gün tatil (örn: 2025-05-01)
        public string? Description { get; set; } // (örn: "1 Mayıs Emek Bayramı")
    }
}
