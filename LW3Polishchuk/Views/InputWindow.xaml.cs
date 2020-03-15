using System.Windows.Controls;
using KMA.ProgrammingInCSharp.LW3Polishchuk.ViewModels;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk.Views
{
    public partial class InputWindow : Page
    {
        public InputWindow()
        {
            InitializeComponent();
            DataContext = new InputWindowViewModel();
        }
    }
}