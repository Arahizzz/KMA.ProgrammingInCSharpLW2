﻿using System.Diagnostics;

namespace LW5Polishchuk.ViewModels
{
    internal class ThreadListViewModel : BaseViewModel
    {
        public ProcessThreadCollection ProcessThreads { get; }

        public ThreadListViewModel(ProcessThreadCollection collection)
        {
            ProcessThreads = collection;
        }
    }
}