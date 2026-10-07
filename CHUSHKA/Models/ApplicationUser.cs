using Microsoft.AspNetCore.Identity;

namespace CHUSHKA.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
