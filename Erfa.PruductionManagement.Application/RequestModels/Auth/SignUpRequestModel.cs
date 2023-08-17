using System.ComponentModel.DataAnnotations;

namespace Erfa.PruductionManagement.Application.RequestModels.Auth
{
    public class SignUpRequestModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Repeated Password is required")]
        public string? RepeatedPassword { get; set; }

        [Required(ErrorMessage = "Registration Code is required")]
        public string? RegCode { get; set; }
    }
}
