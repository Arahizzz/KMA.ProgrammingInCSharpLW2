using System.Windows;
using KMA.ProgrammingInCSharp.LW3Polishchuk.ViewModels;

namespace KMA.ProgrammingInCSharp.LW3Polishchuk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}