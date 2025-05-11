using EasyConnect.Models.Dtos.Base;
using EasyConnect.Models.Entities;

namespace EasyConnect.Models.Dtos
{
    public class BusinessProfileDto : BaseDto
    {
        public string UserId { get; set; }
        public string BusinessName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ProvinceCode { get; set; }
        public byte[] ImageData { get; set; }
    }
}
