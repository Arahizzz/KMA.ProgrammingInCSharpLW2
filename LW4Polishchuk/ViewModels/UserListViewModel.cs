using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using KMA.ProgrammingInCSharp.LW4Polishchuk.Exceptions;
using KMA.ProgrammingInCSharp.LW4Polishchuk.Managers;
using KMA.ProgrammingInCSharp.LW4Polishchuk.Models;
using KMA.ProgrammingInCSharp.LW4Polishchuk.Services;
using KMA.ProgrammingInCSharp.LW4Polishchuk.Views;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.ViewModels
{
    internal class UserListViewModel : BaseViewModel
    {
        #region Fields
        
        private ObservableCollection<PersonViewModel> _users = new ObservableCollection<PersonViewModel>();
        private ObservableCollection<PersonViewModel> _filteredUsers = new ObservableCollection<PersonViewModel>();

        private int _selectedUser;
        private RelayCommand<object> _deleteUserCommand;
        private RelayCommand<object> _addUserCommand;
        private object _lock = new object();

        #region Filters

        private string _name;
        private string _surname;
        private string _email;
        private string _dateTime;
        private bool? _isAdult;
        private bool? _isBirthday;
        private string _sunSignString;
        private string _chineseSignString;
        private RelayCommand<object> _filterUserCommand;

        #endregion
        #endregion

        public UserListViewModel()
        {
            Task.Run(LoadUsers);
        }

        private ObservableCollection<PersonViewModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                FilterUsers();
            }
        }

        public string Name
        {
            private get => _name;
            set => _name = value;
        }

        public string Email
        {
            private get => _email;
            set => _email = value;
        }

        public string Surname
        {
            private get => _surname;
            set => _surname = value;
        }

        public bool? IsAdult
        {
            private get => _isAdult;
            set => _isAdult = value;
        }

        public bool? IsBirthDay
        {
            private get => _isBirthday;
            set => _isBirthday = value;
        }

        public string DateTime
        {
            private get => _dateTime;
            set => _dateTime = value;
        }

        public string ChineseSignString
        {
            private get => _chineseSignString;
            set => _chineseSignString = value;
        }

        public string SunSignString
        {
            private get => _sunSignString;
            set => _sunSignString = value;
        }

        public ObservableCollection<PersonViewModel> FilteredUsers
        {
            get => _filteredUsers;
            set
            {
                _filteredUsers = value;
                OnPropertyChanged();
            }
        }

        public int SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                DeleteUserCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> DeleteUserCommand
        {
            get => _deleteUserCommand ??= new RelayCommand<object>(_ => DeleteUser(), _ => SelectedUser != -1);
        }

        public RelayCommand<object> AddUserCommand
        {
            get => _addUserCommand ??= new RelayCommand<object>(_ => AddUser());
        }

        public RelayCommand<object> SaveUsersCommand
        {
            get => _addUserCommand ??= new RelayCommand<object>(_ => AddUser());
        }
        
        public RelayCommand<object> FilterUsersCommand
        {
            get => _filterUserCommand ??= new RelayCommand<object>(_ => FilterUsers());
        }

        private void DeleteUser()
        {
            Users.Remove(FilteredUsers[SelectedUser]);
            FilteredUsers.RemoveAt(SelectedUser);
        }

        private void AddUser()
        {
            var addUserWindow = new InputWindow(Users);
            addUserWindow.ShowDialog();
            FilterUsers();
            SelectedUser = FilteredUsers.Count - 1;
        }

        private async void Save()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => UsersProvider.SaveUsers(Users));
            LoaderManager.Instance.HideLoader();
        }

        private async void FilterUsers()
        {
            await Task.Run(() =>
            {
                try
                {
                    LoaderManager.Instance.ShowLoader();
                    IEnumerable<PersonViewModel> users = Users;
                    if (IsAdult != null)
                        users = users.Where(s => s.IsAdult == IsAdult);
                    if (IsBirthDay != null)
                        users = users.Where(s => s.IsBirthday == IsBirthDay);
                    if (!string.IsNullOrEmpty(Name))
                        users = users.Where(s => s.Name.Contains(Name));
                    if (!string.IsNullOrEmpty(Surname))
                        users = users.Where(s => s.Surname.Contains(Surname));
                    if (!string.IsNullOrEmpty(Email))
                        users = users.Where(s => s.Email.Contains(Email));
                    if (!string.IsNullOrEmpty(DateTime))
                        users = users.Where(s =>
                        {
                            var date = s.BirthDate;
                            if (System.DateTime.TryParse(DateTime, out var filter))
                                return date.Year == filter.Year && date.Month == filter.Month && date.Day == filter.Day;
                            else throw new FailedToParseException(typeof(DateTime));
                        });
                    if (!string.IsNullOrEmpty(ChineseSignString))
                        users = users.Where(s =>
                        {
                            if (Enum.TryParse<ChineseSign>(ChineseSignString, out var sign))
                                return s.ChineseSign == sign;
                            else throw new FailedToParseException(typeof(ChineseSign));
                        });
                    if (!string.IsNullOrEmpty(SunSignString))
                        users = users.Where(s =>
                        {
                            if (Enum.TryParse<SunSign>(SunSignString, out var sign))
                                return s.SunSign == sign;
                            else throw new FailedToParseException(typeof(SunSign));
                        });
                    var collection = new ObservableCollection<PersonViewModel>(users);
                    FilteredUsers = collection;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    FilteredUsers = new ObservableCollection<PersonViewModel>(Users);
                }
                finally
                {
                    LoaderManager.Instance.HideLoader();
                }
            });
        }

        private async Task LoadUsers()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Users = UsersProvider.GetUsers();
            });
            LoaderManager.Instance.HideLoader();
        }
    }
}