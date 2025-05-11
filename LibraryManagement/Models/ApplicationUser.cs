using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom fields if needed
        public string? FullName { get; set; }
    }
}
