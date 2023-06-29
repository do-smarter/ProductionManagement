namespace Erfa.PruductionManagement.Application.Exceptions
{
    public class EntityUpdateException : Exception
    {
        public EntityUpdateException(string name, object key)
            : base($"{name} ({key}) not modified")
        {
        }
    }
}
