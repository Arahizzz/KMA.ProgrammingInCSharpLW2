using System;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Exceptions
{
    internal class BirthdayInFutureException : ArgumentException
    {
        internal BirthdayInFutureException() : base("User cannot have a birthday date from the future"){}
    }
}