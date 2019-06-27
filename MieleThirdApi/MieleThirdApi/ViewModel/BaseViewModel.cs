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
        //protected IRestApi _restApi { get; }
        protected IGeraeteManager _geraeteManager { get; }
             
        protected BaseViewModel(INavigation navigation)
        { 
            _navigation = navigation;

            _geraeteManager = new GeraeteManager();
        }


        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        #endregion

    }

}
