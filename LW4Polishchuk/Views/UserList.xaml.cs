using System.Globalization;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using KMA.ProgrammingInCSharp.LW4Polishchuk.ViewModels;

namespace KMA.ProgrammingInCSharp.LW4Polishchuk.Views
{
    public partial class UserList : Page
    {
        public UserList()
        {
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            InitializeComponent();
            DataContext = new UserListViewModel();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Selector selector = sender as Selector;
            DataGrid dataGrid = selector as DataGrid;
            if ( dataGrid != null && selector.SelectedItem != null && dataGrid.SelectedIndex >= 0 )
            {
                dataGrid.ScrollIntoView( selector.SelectedItem );
            }
        }
    }
}