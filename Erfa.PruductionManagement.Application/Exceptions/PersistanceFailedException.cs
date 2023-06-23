namespace Erfa.PruductionManagement.Application.Exceptions
{
    public class PersistanceFailedException : Exception
    {
        public PersistanceFailedException(string name, object key)
            : base($"{name} ({key}) is not saved")
        {
        }
    }
}
