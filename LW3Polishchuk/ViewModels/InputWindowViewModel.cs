using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using KMA.ProgrammingInCSharp.LW3Polishchuk.Managers;
using KMA.ProgrammingInCSharp.LW3Polishchuk.Models;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.ViewModels
{
    internal class InputWindowViewModel : BaseViewModel
    {
        #region Fields

        private string _name;

        private string _surname;

        private string _email;

        private DateTime _birthday = DateTime.Today;

        private string _personInfo;

        private RelayCommand<object> _proccedCommand;

        #endregion


        #region Properties

        public RelayCommand<object> ProceedCommand
        {
            get => _proccedCommand ??= new RelayCommand<object>(_ => Proceed(), _ => AllFieldsNotEmpty());
        }

        public string Name
        {
            private get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                ProceedCommand.RaiseCanExecuteChanged();
            }
        }

        public string Surname
        {
            private get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
                ProceedCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            private get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ProceedCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime Birthday
        {
            private get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string PersonInfo
        {
            get => _personInfo;
            private set
            {
                _personInfo = value;
                OnPropertyChanged();
            }
        }

        private Person Person { get; set; }

        #endregion


        private bool AllFieldsNotEmpty()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) &&
                   !string.IsNullOrWhiteSpace(Email);
        }

        private string GetSunSign() => $"Sun Sign: {Person.SunSign}\n";

        private string GetChineseSign() => $"Chinese Sign: {Person.ChineseSign}\n";

        private string GetAgeInfo()
        {
            string isBirthday = Person.IsBirthday
                ? "Happy Birthday!🎂\n"
                : string.Empty;
            string isAdult = Person.IsAdult ? "You are an adult\n" : "You are not an adult\n";
            return isBirthday + $"Your birth date is {Birthday:d}\n" + isAdult;
        }

        private string GetEmail() => $"Email: {Person.Email}\n";

        private string GetName() => $"Name: {Person.Name} {Person.Surname}\n";

        private async Task<string> GetPersonInfo()
        {
            var name = Task.Run(GetName);
            var ageInfo = Task.Run(GetAgeInfo);
            var email = Task.Run(GetEmail);
            var sunSign = Task.Run(GetSunSign);
            var chineseSign = Task.Run(GetChineseSign);
            return (await Task.WhenAll(name, ageInfo, email, sunSign, chineseSign))
                .Aggregate((f, s) => f + s);
        }

        private async void Proceed()
        {
            try
            {
                LoaderManager.Instance.ShowLoader();
                PersonInfo = string.Empty;
                Person = await Task.Run(() => new Person(Name, Surname, Email, Birthday));
                PersonInfo = await GetPersonInfo();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                LoaderManager.Instance.HideLoader();
            }
        }
    }
}