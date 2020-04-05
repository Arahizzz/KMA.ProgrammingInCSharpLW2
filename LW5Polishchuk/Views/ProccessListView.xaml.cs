using System.Windows.Controls;
using LW5Polishchuk.ViewModels;

namespace LW5Polishchuk.Views
{
    public partial class ProccessListView : Page
    {
        public ProccessListView()
        {
            InitializeComponent();
            DataContext = new ProccessListViewModel();
        }
    }
}