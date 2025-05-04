using EasyConnect.Models.Entities.Base;
using EasyConnect.Models.Enums;

namespace EasyConnect.Models.Entities
{
    public class Appointment : BaseEntity
    {
        public int BusinessProfileId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan Duration { get; set; }

        // Kullanıcı (müşteri) bağlantısı
        public string UserId { get; set; }
        public User User { get; set; }

        public string CustomerPhone { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    }
}
