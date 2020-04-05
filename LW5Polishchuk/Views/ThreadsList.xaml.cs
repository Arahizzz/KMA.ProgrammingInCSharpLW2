using System.Diagnostics;
using System.Windows;
using LW5Polishchuk.ViewModels;

namespace LW5Polishchuk.Views
{
    public partial class ThreadsList : Window
    {
        public ThreadsList(ProcessThreadCollection collection)
        {
            InitializeComponent();
            
            DataContext = new ThreadListViewModel(collection);
        }
    }
}