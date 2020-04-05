using System;
using System.Diagnostics;
using System.Management;
using System.Threading.Tasks;

namespace LW5Polishchuk.ViewModels
{
    internal class ProcessViewModel : BaseViewModel, IEquatable<ProcessViewModel>
    {
        private static long RamSize = (long)new PerformanceCounter("Memory", "Available MBytes").NextValue();
        private Process _process;
        private TimeSpan _prev = TimeSpan.Zero;
        private string _user = "Loading...";

        public ProcessViewModel(Process process)
        {
            _process = process;
            Task.Run(GetUserInfo);
        }

        public Process Process
        {
            get => _process;
            set
            {
                _process = value;
                OnPropertyChanged("Cpu");
                OnPropertyChanged("Ram");
                OnPropertyChanged("RamPercent");
                OnPropertyChanged("Threads");
            }
        }

        public int Id => _process.Id;

        public string Name => _process.ProcessName;

        public string Cpu
        {
            get
            {
                try
                {
                    var curr = _process.TotalProcessorTime;
                    var delta = curr - _prev;
                    _prev = curr;
                    return (delta.TotalMilliseconds / 100).ToString();
                }
                catch
                {
                    return "NaN";
                }
            }
        }

        public long Ram => _process.WorkingSet64 / 1_000_000;

        public float RamPercent => _process.WorkingSet64 / 1_000_000f / RamSize * 100;

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

        public string Path
        {
            get
            {
                try
                {
                    return Process.MainModule?.FileName;
                }
                catch
                {
                    return null;
                }
            }
        }

        public DateTime? StartTime
        {
            get
            {
                try
                {
                    return Process.StartTime;
                }
                catch
                {
                    return null;
                }
            }
        }
        public bool Equals(ProcessViewModel other) => Id.Equals(other?.Id);

        private void GetUserInfo()
        {
            try
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
                        User = argList[1] + "\\" + argList[0];
                        return;
                    }
                }
            }
            catch
            {
                User = "NO OWNER";
            }

            User = "NO OWNER";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}