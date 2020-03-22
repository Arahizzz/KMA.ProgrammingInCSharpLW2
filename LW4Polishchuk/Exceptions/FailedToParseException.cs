using System;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Exceptions
{
    internal class FailedToParseException : ArgumentException
    {
        public FailedToParseException(Type type) : base($"Failed to parse {type.Name}")
        {
        }
    }
}