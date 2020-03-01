using System.Windows.Controls;
using KMA.ProgrammingInCSharp.LW2Polishchuk.ViewModels;

namespace KMA.ProgrammingInCSharp.LW2Polishchuk.Views
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