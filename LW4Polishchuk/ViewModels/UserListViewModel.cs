using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using KMA.ProgrammingInCSharp.LW3Polishchuk.Managers;
using KMA.ProgrammingInCSharp.LW3Polishchuk.Services;
using KMA.ProgrammingInCSharp.LW3Polishchuk.Views;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.ViewModels
{
    internal class UserListViewModel : BaseViewModel
    {
        private ObservableCollection<PersonViewModel> _users = new ObservableCollection<PersonViewModel>();

        private string _filter = string.Empty;

        private int _selectedUser;
        private RelayCommand<object> _deleteUserCommand;
        private RelayCommand<object> _addUserCommand;

        public UserListViewModel()
        {
            Task.Run(LoadUsers);
        }

        public ObservableCollection<PersonViewModel> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public string Filter
        {
            get => _filter;
            set => _filter = value;
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

        private void DeleteUser() => Users.RemoveAt(SelectedUser);

        private void AddUser()
        {
            var addUserWindow = new InputWindow(_users);
            addUserWindow.ShowDialog();
            SelectedUser = Users.Count - 1;
        }

        private async void Save()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => UsersProvider.SaveUsers(Users));
            LoaderManager.Instance.HideLoader();
        }
        private async Task LoadUsers()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Users = UsersProvider.GetUsers());
            LoaderManager.Instance.HideLoader();
        }
    }
}