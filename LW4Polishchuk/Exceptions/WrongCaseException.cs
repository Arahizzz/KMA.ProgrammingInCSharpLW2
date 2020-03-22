using System;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Exceptions
{
    internal class WrongCaseException : ArgumentException
    {
        internal WrongCaseException() : base("Name and surname should begin with capital letter"){}
    }
}