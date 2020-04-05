using System;
using System.Diagnostics;
using System.Management;
using System.Threading.Tasks;

namespace LW5Polishchuk.ViewModels
{
    internal class ProcessViewModel : BaseViewModel, IEquatable<ProcessViewModel>
    {
        private Process _process;
        private TimeSpan _prev = TimeSpan.Zero;
        private string _user = "Loading...";

        public ProcessViewModel(Process process)
        {
            _process = process;
            Task.Run(GetInfo);
        }

        public Process Process
        {
            get => _process;
            set
            {
                _process = value;
                OnPropertyChanged("Cpu");
                OnPropertyChanged("Ram");
                OnPropertyChanged("Threads");
            }
        }

        public int Id => _process.Id;

        public string Name => _process.ProcessName;

        public double Cpu
        {
            get
            {
                var curr = _process.TotalProcessorTime;
                var delta = curr - _prev;
                _prev = curr;
                return delta.TotalMilliseconds / 100;
            }
        }

        public long Ram => _process.WorkingSet64 / 1_000_000;

        public int Threads => _process.Threads.Count;

        public string User
        {
            get => _user;
            private set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public string Path => Process.MainModule?.FileName;

        public DateTime StartTime => _process.StartTime;
        public bool Equals(ProcessViewModel other) => Id.Equals(other?.Id);

        public void GetInfo()
        {
            string query = "Select * From Win32_Process Where ProcessID = " + Process.Id;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    User = argList[1] + "\\" + argList[0];
                    return;
                }
            }

            User = "NO OWNER";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}