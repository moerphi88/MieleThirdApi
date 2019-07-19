using MieleThirdApi.Data;
using MieleThirdApi.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MieleThirdApi.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigation _navigation { get; }
        protected IRestApi _restApi { get; }
        protected IGeraeteManager _geraeteManager { get; }
        protected ILoginManager _loginManager;

        protected BaseViewModel(INavigation navigation, ILoginManager loginManager)
        { 
            _navigation = navigation;

            //Hier kann später die Compiler MOCK Abfrage rein
            //_restApi = new RestMockService();
            _restApi = new RestApiService();
            
            //Wenn ich das hier erstelle, dann wird ja für jeden View, der von BaseVM erbt ein neuer Gerätemanager angelegt. Nicht gut
            _geraeteManager = new GeraeteManager(_restApi);

            _loginManager = loginManager;
        }

        protected virtual async Task OnLoggedOut(object sender, EventArgs e)
        {
            await _navigation.PushModalAsync(new LoginView(_loginManager));
        }

        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        #endregion

    }

}
