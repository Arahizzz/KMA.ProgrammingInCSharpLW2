using System;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Exceptions
{
    internal class BirthdayInFutureException : ArgumentException
    {
        internal BirthdayInFutureException() : base("User cannot have a birthday date from the future"){}
    }
}