using EasyConnect.Models.Dtos.Base;

namespace EasyConnect.Models.Dtos
{
    public class ServiceDto : BaseDto
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
    }
}
