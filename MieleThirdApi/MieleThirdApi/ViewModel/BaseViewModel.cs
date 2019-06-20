using MieleThirdApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MieleThirdApi.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigation _navigation { get; }
        protected IRestApi _restApi { get; }
             
        protected BaseViewModel(INavigation navigation)
        { 
            _navigation = navigation;

            //Hier kann später die Compiler MOCK Abfrage rein
            _restApi = new RestMockService();
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        #endregion

    }

}
