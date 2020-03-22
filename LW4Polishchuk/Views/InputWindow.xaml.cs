using System.Collections.ObjectModel;
using System.Windows;
using KMA.ProgrammingInCSharp.LW4Polishchuk.ViewModels;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Views
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