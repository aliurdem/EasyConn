using EasyConnect.Models.Dtos.Base;

namespace EasyConnect.Models.Dtos
{
    public class StaffDto : BaseDto
    {
        public string FullName { get; set; }
        public int BusinessProfileId { get; set; }
    }
}
