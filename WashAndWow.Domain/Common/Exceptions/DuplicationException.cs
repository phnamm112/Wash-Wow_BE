namespace Wash_Wow.Domain.Common.Exceptions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string message) : base(message)
        {
        }
    }
}
