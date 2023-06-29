namespace Erfa.PruductionManagement.Application.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string userName)
            : base($"{userName} is not uthorized for this resource")
        {
        }
    }
}
