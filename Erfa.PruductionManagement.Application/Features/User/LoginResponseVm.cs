namespace Erfa.PruductionManagement.Application.Features.User
{
    public class LoginResponseVm
    {
        public DateTime Expires { get; set; }
        public string[] Roles { get; set; } = new string[] { };
        public string Username { get; set; } = string.Empty;
    }
}
