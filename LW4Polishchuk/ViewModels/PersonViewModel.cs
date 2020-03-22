using System;
using System.Windows;
using KMA.ProgrammingInCSharp.LW3Polishchuk.Models;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.ViewModels
{
    internal class PersonViewModel : BaseViewModel
    {
        private readonly Person _person;

        public PersonViewModel(Person person)
        {
            _person = person;
        }

        public Person Person => _person;

        public string Name
        {
            get => _person.Name;
            set
            {
                try
                {
                    _person.Name = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public string Surname
        {
            get => _person.Surname;
            set
            {
                try
                {
                    _person.Surname = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public string Email
        {
            get => _person.Email;
            set
            {
                try
                {
                    _person.Email = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public DateTime BirthDate
        {
            get => _person.BirthDate;
            set
            {
                try
                {
                    _person.BirthDate = value;
                    OnPropertyChanged("IsAdult");
                    OnPropertyChanged("IsBirthday");
                    OnPropertyChanged("SunSign");
                    OnPropertyChanged("ChineseSign");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public bool IsAdult => _person.IsAdult;

        public SunSign SunSign => _person.SunSign;

        public ChineseSign ChineseSign => _person.ChineseSign;

        public bool IsBirthday => _person.IsBirthday;
    }
}