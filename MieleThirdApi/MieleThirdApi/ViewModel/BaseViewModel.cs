using MieleThirdApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MieleThirdApi.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigation _navigation { get; }
        protected IRestApi _restApi { get; }
        protected IGeraeteManager _geraeteManager { get; }
             
        protected BaseViewModel(INavigation navigation)
        { 
            _navigation = navigation;

            //Hier kann später die Compiler MOCK Abfrage rein
            //_restApi = new RestMockService();
            _restApi = new RestApiService();

            _geraeteManager = new GeraeteManager(_restApi);
        }


        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        #endregion

    }

}
