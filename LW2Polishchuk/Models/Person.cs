using System;

namespace KMA.ProgrammingInCSharp.LW2Polishchuk.Models
{
    public class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

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
                    case 1: sun = BirthDate.Day <= 20 ? SunSign.Capricorn : SunSign.Aquarius; break;
                    case 2: sun = BirthDate.Day <= 18 ? SunSign.Aquarius : SunSign.Pisces; break;
                    case 3: sun = BirthDate.Day <= 20 ? SunSign.Pisces : SunSign.Aries; break;
                    case 4: sun = BirthDate.Day <= 20 ? SunSign.Aries : SunSign.Taurus; break;
                    case 5: sun = BirthDate.Day <= 21 ? SunSign.Taurus : SunSign.Gemini; break;
                    case 6: sun = BirthDate.Day <= 21 ? SunSign.Gemini : SunSign.Cancer; break;
                    case 7: sun = BirthDate.Day <= 22 ? SunSign.Cancer : SunSign.Leo; break;
                    case 8: sun = BirthDate.Day <= 23 ? SunSign.Leo : SunSign.Virgo; break;
                    case 9: sun = BirthDate.Day <= 22 ? SunSign.Virgo : SunSign.Libra; break;
                    case 10: sun = BirthDate.Day <= 23 ? SunSign.Libra : SunSign.Scorpio; break;
                    case 11: sun = BirthDate.Day <= 22 ? SunSign.Scorpio : SunSign.Sagittarius; break;
                    case 12: sun = BirthDate.Day <= 21 ? SunSign.Sagittarius : SunSign.Capricorn; break;
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