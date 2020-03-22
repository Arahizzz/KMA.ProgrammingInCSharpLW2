using System;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Exceptions
{
    internal class PersonIsTooOldException : ArgumentException
    {
        internal PersonIsTooOldException() : base("Person should be younger than 135 years old"){}
    }
}