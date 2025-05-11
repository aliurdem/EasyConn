using EasyConnect.Application.Services.Base;
using EasyConnect.Models.Entities.Base;

namespace EasyConnect.Models.Entities
{
    public class BusinessProfile : BaseEntity
    {
        public string UserId { get; set; }                
        public User User { get; set; } 
        public string BusinessName { get; set; } 
        public string Phone { get; set; } 
        public string Address { get; set; }
        public int ProvinceCode { get; set; }
        public int CategoryId { get; set; }
        public byte[] ImageData { get; set; }

        public Category Category { get; set; }
        public ICollection<Staff> Staffs { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Holiday> Holidays { get; set; } = new List<Holiday>();
    }
}
