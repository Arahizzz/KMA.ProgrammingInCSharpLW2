using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using LW5Polishchuk.Views;

namespace LW5Polishchuk.ViewModels
{
    internal class ProccessListViewModel : BaseViewModel
    {
        private static readonly ProccessEqual cmp = new ProccessEqual();
        private readonly Timer _updateListTimer = new Timer(5000);
        private readonly Timer _refreshInfoTimer = new Timer(2000);
        private ObservableCollection<ProcessViewModel> _processes;
        private RelayCommand<object> _viewThreads;
        private RelayCommand<object> _viewModules;
        private RelayCommand<object> _killProcess;
        private RelayCommand<object> _openFolder;
        private int _selectedProcess = -1;

        public ProccessListViewModel()
        {
            Processes = new ObservableCollection<ProcessViewModel>(Process.GetProcesses()
                .Select(p => new ProcessViewModel(p)));
            _updateListTimer.Elapsed += UpdateList;
            _updateListTimer.AutoReset = true;
            _updateListTimer.Enabled = true;
            _refreshInfoTimer.Elapsed += RefreshList;
            _refreshInfoTimer.AutoReset = true;
            _refreshInfoTimer.Enabled = true;
        }
        public int SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                _selectedProcess = value;
                ViewThreads.RaiseCanExecuteChanged();
                ViewModules.RaiseCanExecuteChanged();
                KillProcess.RaiseCanExecuteChanged();
                OpenFolder.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand<object> ViewThreads
        {
            get => _viewThreads ??= new RelayCommand<object>(
                _ => ShowThreads(), _ => SelectedProcess != -1);
        }

        public RelayCommand<object> ViewModules
        {
            get => _viewModules ??= new RelayCommand<object>(
                _ => ShowModules(), _ => SelectedProcess != -1);
        }

        public RelayCommand<object> KillProcess
        {
            get => _killProcess ??= new RelayCommand<object>(
                _ => KillProc(), _ => SelectedProcess != -1);
        }

        public RelayCommand<object> OpenFolder
        {
            get => _openFolder ??= new RelayCommand<object>(
                _ => OpenExplorer(), _ => SelectedProcess != -1 && !string.IsNullOrEmpty(Processes[SelectedProcess].Path));
        }
        
        public ObservableCollection<ProcessViewModel> Processes
        {
            get => _processes;
            private set
            {
                _processes = value;
                OnPropertyChanged();
            }
        }
        

        private void ShowModules()
        {
            try
            {
                var modules = Processes[SelectedProcess].Process.Modules;
                var addUserWindow = new ModulesList(modules);
                addUserWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Cannot get modules of this process");
            }
        }

        private void ShowThreads()
        {
            var modules = Processes[SelectedProcess].Process.Threads;
            var addUserWindow = new ThreadsList(modules);
            addUserWindow.ShowDialog();
        }

        private void KillProc()
        {
            try
            {
                var proc = Processes[SelectedProcess].Process;
                if (!proc.CloseMainWindow())
                {
                    proc.Kill();
                }
            }
            catch
            {
                MessageBox.Show("Cannot kill this process");
            }
        }

        private void OpenExplorer()
        {
            string args = string.Format("/e, /select, \"{0}\"", Processes[SelectedProcess].Path);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = args;
            Process.Start(info);
        }

        private void UpdateList(Object source, ElapsedEventArgs e)
        {
            var dict = Processes.ToDictionary(p => p.Id);
            var items = Process.GetProcesses();
            var toDelete = Processes
                .Select(p => p.Process)
                .Except(items, cmp)
                .Where(p => p != null)
                .Select(p => dict[p.Id])
                .ToList();

            var toAdd = new List<ProcessViewModel>();
            foreach (var item in items)
            {
                if (!dict.ContainsKey(item.Id)) toAdd.Add(new ProcessViewModel(item));
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                foreach (var process in toDelete)
                {
                    Processes.Remove(process);
                }

                foreach (var processViewModel in toAdd)
                {
                    Processes.Add(processViewModel);
                }
            }));
        }

        private void RefreshList(Object source, ElapsedEventArgs e)
        {
            var dict = Processes.ToDictionary(p => p.Id);
            foreach (var process in Process.GetProcesses())
            {
                if (dict.ContainsKey(process.Id))
                    dict[process.Id].Process = process;
            }
        }

        private class ProccessEqual : IEqualityComparer<Process>
        {
            public bool Equals(Process x, Process y)
            {
                if (x == null)
                    return y == null;
                return x.Id.Equals(y?.Id);
            }

            public int GetHashCode(Process obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}