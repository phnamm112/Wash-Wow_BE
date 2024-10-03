namespace Wash_Wow.Domain.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
