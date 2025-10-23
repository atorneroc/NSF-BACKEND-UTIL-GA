namespace Scharff.Domain.Utils.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) : base(message)
        {
        }
    }
}
