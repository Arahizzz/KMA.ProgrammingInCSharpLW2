using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using GalaSoft.MvvmLight.CommandWpf;
using LW5Polishchuk.Views;

namespace LW5Polishchuk.ViewModels
{
    internal class ProccessListViewModel : BaseViewModel
    {
        public ObservableCollection<ProcessViewModel> Processes
        {
            get => _processes;
            set
            {
                _processes = value;
                OnPropertyChanged();
            }
        }

        private readonly Timer _updateListTimer = new Timer(5000);
        private readonly Timer _refreshInfoTimer = new Timer(2000);
        private ObservableCollection<ProcessViewModel> _processes;
        private GalaSoft.MvvmLight.Command.RelayCommand<object> _viewThreads;
        private GalaSoft.MvvmLight.Command.RelayCommand<object> _viewModules;
        private int _selectedProcess = -1;

        public int SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                _selectedProcess = value;
                OnPropertyChanged();
            }
        }

        public GalaSoft.MvvmLight.Command.RelayCommand<object> ViewThreads
        {
            get => _viewThreads ??= new GalaSoft.MvvmLight.Command.RelayCommand<object>(
                _ => ShowThreads(), _ => SelectedProcess != -1);
        }

        public GalaSoft.MvvmLight.Command.RelayCommand<object> ViewModules
        {
            get => _viewModules ??= new GalaSoft.MvvmLight.Command.RelayCommand<object>(
                _ => ShowModules(), _ => SelectedProcess != -1);
        }

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

        private void ShowModules()
        {
            var modules = Processes[SelectedProcess].Process.Modules;
            var addUserWindow = new ModulesList(modules);
            addUserWindow.ShowDialog();
        }

        private void ShowThreads()
        {
            var modules = Processes[SelectedProcess].Process.Threads;
            var addUserWindow = new ThreadsList(modules);
            addUserWindow.ShowDialog();
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

        private static readonly ProccessEqual cmp = new ProccessEqual();

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