using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KMA.ProgrammingInCSharp.LW2Polishchuk.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}