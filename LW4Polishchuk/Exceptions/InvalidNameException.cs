using System;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Exceptions
{
    internal class InvalidNameException : ArgumentException
    {
        internal InvalidNameException() : base("Name and surname should contain only letters"){}
    }
}