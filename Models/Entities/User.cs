using EasyConnect.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace EasyConnect.Models.Entities
{
    public class User : IdentityUser
    {
        public UserType UserType { get; set; } = UserType.Customer;
        public string? FullName { get; set; }         
        public BusinessProfile? BusinessProfile { get; set; }
    }
}
