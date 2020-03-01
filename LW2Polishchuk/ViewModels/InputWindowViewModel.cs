﻿using System;
 using System.Threading.Tasks;
 using System.Windows;
 using GalaSoft.MvvmLight.Command;
 using KMA.ProgrammingInCSharp.LW2Polishchuk.Models;

 namespace KMA.ProgrammingInCSharp.LW2Polishchuk.ViewModels
{
    public class InputWindowViewModel : BaseViewModel
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
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                ProceedCommand.RaiseCanExecuteChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
                ProceedCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ProceedCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string PersonInfo
        {
            get => _personInfo;
            set
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

        private bool IsCorrectDate()
        {
            bool success;
            var now = DateTime.Now;
            if (now.Year - Birthday.Year > 135)
                success = false;
            else
            {
                var delta = now - Birthday;
                success = delta.Milliseconds > 0;
            }

            if (!success)
            {
                MessageBox.Show("Incorrect Birth date.");
            }

            return success;
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

        private string GetPersonInfo()
        {
            return GetName() + GetEmail() + GetAgeInfo() + GetSunSign() + GetChineseSign();
        }

        private async void Proceed()
        {
            if (await Task.Run(IsCorrectDate))
            {
                Person = new Person(Name, Surname, Email, Birthday);
                var info = await Task.Run(GetPersonInfo);
                PersonInfo = info;
            }
        }
    }
}