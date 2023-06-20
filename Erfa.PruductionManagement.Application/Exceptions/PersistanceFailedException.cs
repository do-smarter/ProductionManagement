namespace Erfa.PruductionManagement.Application.Exceptions
{
    internal class PersistanceFailedException : Exception
    {
        public PersistanceFailedException(string name, object key)
            : base($"{name} ({key}) is not saved")
        {
        }
    }
}
