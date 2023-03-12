using System.Globalization;

namespace Mikro.Task.Services.Application.Helpers
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

       
    }

    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("Not authorized") { }

        public NotAuthorizedException(string message) : base(message) { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Not Found") { }

        public NotFoundException(string message) : base(message) { }
    }
}
