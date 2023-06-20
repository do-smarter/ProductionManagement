namespace Erfa.PruductionManagement.Application.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string name, object key)
            : base($"{name} ({key}) is not found")
        {
        }
    }
}
