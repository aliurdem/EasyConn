namespace EasyConnect.Models.Dtos
{
    public class BusinessProfileServiceAssignDto
    {
        public int BusinessProfileId { get; set; }
        public List<int> ServiceIds { get; set; } = new();

    }
}
