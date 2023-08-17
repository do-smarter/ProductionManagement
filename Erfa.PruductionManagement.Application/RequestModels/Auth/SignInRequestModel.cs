using System.ComponentModel.DataAnnotations;

namespace Erfa.PruductionManagement.Application.RequestModels.Auth
{
    public class SignInRequestModel
    {
        [Required(ErrorMessage = "User name is required ")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required ")]
        public string? Password { get; set; }
    }
}
