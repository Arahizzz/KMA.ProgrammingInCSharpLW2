using System;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Exceptions
{
    internal class PersonIsTooOldException : ArgumentException
    {
        internal PersonIsTooOldException() : base("Person should be younger than 135 years old"){}
    }
}