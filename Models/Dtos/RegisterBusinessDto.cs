namespace EasyConnect.Models.Dtos
{
    public class RegisterBusinessDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string? FullName { get; set; }
        public string? BusinessName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
