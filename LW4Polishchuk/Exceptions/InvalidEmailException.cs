﻿using System;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Exceptions
{
    internal class InvalidEmailException : ArgumentException
    {
        internal InvalidEmailException(string email) : base(
            $"Invalid email format for input {email}. Input should be in format email@site.com"){}
    }
}