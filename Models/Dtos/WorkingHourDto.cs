using EasyConnect.Models.Dtos.Base;
using EasyConnect.Models.Entities;

namespace EasyConnect.Models.Dtos
{
    public class WorkingHourDto : BaseDto
    {
        public int BusinessProfileId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
