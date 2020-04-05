using System.Diagnostics;
using System.Windows;
using LW5Polishchuk.ViewModels;

namespace LW5Polishchuk.Views
{
    public partial class ModulesList : Window
    {
        public ModulesList(ProcessModuleCollection collection)
        {
            InitializeComponent();
            
            DataContext = new ModuleListViewModel(collection);
        }
    }
}