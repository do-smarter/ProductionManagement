namespace Erfa.PruductionManagement.Application.Exceptions
{
    public class EntityUnmodifiedException : Exception
    {
        public EntityUnmodifiedException(string name, object key)
            : base($"{name} ({key}) not modified")
        {
        }
    }
}
