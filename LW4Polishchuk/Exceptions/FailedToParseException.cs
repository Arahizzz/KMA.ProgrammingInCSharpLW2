using System;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Exceptions
{
    public class FailedToParseException : ArgumentException
    {
        public FailedToParseException(Type type) : base($"Failed to parse {type.Name}")
        {
        }
    }
}