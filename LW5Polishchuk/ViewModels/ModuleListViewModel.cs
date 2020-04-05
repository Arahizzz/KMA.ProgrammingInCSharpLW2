using System.Diagnostics;

namespace LW5Polishchuk.ViewModels
{
    internal class ModuleListViewModel : BaseViewModel
    {
        public ProcessModuleCollection ProcessModules { get; }

        public ModuleListViewModel(ProcessModuleCollection collection)
        {
            ProcessModules = collection;
        }
    }
}