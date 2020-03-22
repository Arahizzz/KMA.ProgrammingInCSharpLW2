using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KMA.ProgrammingInCSharp.LW3Polishchuk.ViewModels;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Views
{
    public partial class InputWindow : Window
    {
        internal InputWindow(ObservableCollection<PersonViewModel> list)
        {
            InitializeComponent();
            DataContext = new InputWindowViewModel(list);
        }
    }
}