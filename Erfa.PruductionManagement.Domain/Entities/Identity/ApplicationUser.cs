using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Erfa.PruductionManagement.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? RegCodeHash { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }
        public bool SignedUp { get; set; } = false;
        public bool Active { get; set; } = false;
    }
}
