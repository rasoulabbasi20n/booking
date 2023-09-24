using System.Runtime.Serialization;

namespace Framework.Gaurds
{
    public class DomainException : Exception
    {
        public DomainException(string? message) : base($"Domain exception occured: '{message}'")
        {
        }
    }
}
