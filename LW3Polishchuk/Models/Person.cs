﻿using System;
using System.Text.RegularExpressions;
using KMA.ProgrammingInCSharp.LW3Polishchuk.Exceptions;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Models
{
    internal class Person
    {
        #region Fields

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                ValidateNameAndSurname(value);
                ValidateCase(value);
                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                ValidateNameAndSurname(value);
                ValidateCase(value);
                _surname = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                ValidateEmail(value);
                _email = value;
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                ValidateDate(value);
                _birthDate = value;
            }
        }

        #endregion

        #region Constructors

        public Person(string name, string surname, string email, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
        }

        public Person(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

        public Person(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

        #endregion

        private static void ValidateDate(DateTime input)
        {
            var now = DateTime.Now;
            if (now.Year - input.Year > 135)
                throw new PersonIsTooOldException();
            var delta = now - input;
            if (delta.Milliseconds < 0)
                throw new BirthdayInFutureException();
        }

        private static void ValidateEmail(string input)
        {
            if (!Regex.IsMatch(input, "^\\w+@\\w+[.]\\w+$"))
                throw new InvalidEmailException(input);
        }

        private static void ValidateCase(string input)
        {
            if (char.IsLower(input, 0))
                throw new WrongCaseException();
        }

        private static void ValidateNameAndSurname(string input)
        {
            if (!Regex.IsMatch(input, "^\\p{L}+$"))
                throw new InvalidNameException();
        }
        public bool IsAdult
        {
            get
            {
                var now = DateTime.Now;
                var years = now.Year - BirthDate.Year;
                if (years >= 18)
                    return true;
                if (years == 17)
                {
                    if (now.Month == BirthDate.Month)
                        return now.Day >= BirthDate.Day;
                    return now.Month > BirthDate.Month;
                }

                return false;
            }
        }

        public SunSign SunSign
        {
            get
            {
                SunSign sun;
                switch (BirthDate.Month)
                {
                    case 1:
                        sun = BirthDate.Day <= 20 ? SunSign.Capricorn : SunSign.Aquarius;
                        break;
                    case 2:
                        sun = BirthDate.Day <= 18 ? SunSign.Aquarius : SunSign.Pisces;
                        break;
                    case 3:
                        sun = BirthDate.Day <= 20 ? SunSign.Pisces : SunSign.Aries;
                        break;
                    case 4:
                        sun = BirthDate.Day <= 20 ? SunSign.Aries : SunSign.Taurus;
                        break;
                    case 5:
                        sun = BirthDate.Day <= 21 ? SunSign.Taurus : SunSign.Gemini;
                        break;
                    case 6:
                        sun = BirthDate.Day <= 21 ? SunSign.Gemini : SunSign.Cancer;
                        break;
                    case 7:
                        sun = BirthDate.Day <= 22 ? SunSign.Cancer : SunSign.Leo;
                        break;
                    case 8:
                        sun = BirthDate.Day <= 23 ? SunSign.Leo : SunSign.Virgo;
                        break;
                    case 9:
                        sun = BirthDate.Day <= 22 ? SunSign.Virgo : SunSign.Libra;
                        break;
                    case 10:
                        sun = BirthDate.Day <= 23 ? SunSign.Libra : SunSign.Scorpio;
                        break;
                    case 11:
                        sun = BirthDate.Day <= 22 ? SunSign.Scorpio : SunSign.Sagittarius;
                        break;
                    case 12:
                        sun = BirthDate.Day <= 21 ? SunSign.Sagittarius : SunSign.Capricorn;
                        break;
                    default: goto case 1;
                }

                return sun;
            }
        }

        public ChineseSign ChineseSign => (ChineseSign) (BirthDate.Year % 12);

        public bool IsBirthday
        {
            get
            {
                var now = DateTime.Now;
                return now.Day == BirthDate.Day && now.Month == BirthDate.Month;
            }
        }
    }
}