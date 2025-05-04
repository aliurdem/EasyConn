using EasyConnect.Models.Entities.Base;

namespace EasyConnect.Models.Entities
{
    public class WorkingHour : BaseEntity
    {
        public int BusinessProfileId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }

        public DayOfWeek DayOfWeek { get; set; } 
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
