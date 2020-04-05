using System.Diagnostics;

namespace LW5Polishchuk.ViewModels
{
    internal class ModuleListViewModel : BaseViewModel
    {
        public ProcessModuleCollection Modules { get; }

        public ModuleListViewModel(ProcessModuleCollection collection)
        {
            Modules = collection;
        }
    }
}