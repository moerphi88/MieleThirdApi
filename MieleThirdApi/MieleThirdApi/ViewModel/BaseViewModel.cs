using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MieleThirdApi.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigation _navigation { get; }   

        protected BaseViewModel(INavigation navigation)
        { 
            _navigation = navigation;
        }
        
        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        #endregion

    }

}
