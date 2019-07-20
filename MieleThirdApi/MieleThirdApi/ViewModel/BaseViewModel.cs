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
        protected static IRestApi _restApi { get; private set; }
        protected static IGeraeteManager _geraeteManager { get; private set; }
        

        protected BaseViewModel(INavigation navigation)
        { 
            _navigation = navigation;

            //Hier kann später die Compiler MOCK Abfrage rein
            //_restApi = new RestMockService();
            _restApi = _restApi ?? new RestApiService();
            
            //Wenn ich das hier erstelle, dann wird ja für jeden View, der von BaseVM erbt ein neuer Gerätemanager angelegt. Nicht gut!
            // Durch die Umstellung auf static und die ?? Abfrage wird jetzt nur noch ein Object erstellt.
            _geraeteManager = _geraeteManager ?? new GeraeteManager(_restApi);

            
            //_loginManager.LoggedOut += OnLoggedOut;
        }

        //protected virtual Task OnLoggedOut(object sender, EventArgs e)
        //{
        //    _navigation.PushModalAsync(new LoginView(_loginManager));
        //}

        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        #endregion

    }

}
