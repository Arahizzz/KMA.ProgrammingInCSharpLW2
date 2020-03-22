using System;
using System.Collections.ObjectModel;
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

        private RelayCommand<object> _proccedCommand;

        public InputWindowViewModel(ObservableCollection<PersonViewModel> users)
        {
            Users = users;
            ProceedCommand = new RelayCommand<Window>(w => Proceed(w), _ => AllFieldsNotEmpty());
        }

        #endregion


        #region Properties

        public RelayCommand<Window> ProceedCommand { get; }

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

        public RelayCommand<Window> CloseWindowCommand { get; private set; }
        private ObservableCollection<PersonViewModel> Users { get; set; }

        #endregion


        private bool AllFieldsNotEmpty()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) &&
                   !string.IsNullOrWhiteSpace(Email);
        }

        private async void Proceed(Window window)
        {
            try
            {
                LoaderManager.Instance.ShowLoader();
                var person = await Task.Run(() => new Person(Name, Surname, Email, Birthday));
                Users.Add(new PersonViewModel(person));
                window.Close();
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