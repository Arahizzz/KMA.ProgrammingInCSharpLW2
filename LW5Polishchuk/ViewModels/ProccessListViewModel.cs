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

namespace LW5Polishchuk.ViewModels
{
    internal class ProccessListViewModel : BaseViewModel
    {
        public ObservableCollection<ProcessViewModel> Processes { get; set; } =
            new ObservableCollection<ProcessViewModel>(Process.GetProcesses().Select(p => new ProcessViewModel(p)));

        private readonly Timer _updateListTimer = new Timer(5000);
        private readonly Timer _refreshInfoTimer = new Timer(2000);
        
        private ListSortDirection lastSortDirection;

        private string lastSortMemberPath;
        private RelayCommand<DataGridSortingEventArgs> _sortCommand;
        private ICollectionView _collectionView;

        public int SelectedProccess { get; set; }

        public ProccessListViewModel()
        {
            _updateListTimer.Elapsed += UpdateList;
            _updateListTimer.AutoReset = true;
            _updateListTimer.Enabled = true;
            _refreshInfoTimer.Elapsed += RefreshList;
            _refreshInfoTimer.AutoReset = true;
            _refreshInfoTimer.Enabled = true;
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