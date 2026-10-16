using Microsoft.AspNetCore.Identity;

namespace EnglishVocabulary.API.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
