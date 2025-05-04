using EasyConnect.Models.Dtos.Base;
using EasyConnect.Models.Entities;
using EasyConnect.Models.Enums;

namespace EasyConnect.Models.Dtos
{
    public class AppointmentDto : BaseDto
    {
        public int BusinessProfileId { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string CustomerPhone { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    }
}
