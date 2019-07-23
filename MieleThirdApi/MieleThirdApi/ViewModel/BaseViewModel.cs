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

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        protected BaseViewModel(INavigation navigation)
        { 
            _navigation = navigation;

            //Hier kann später die Compiler MOCK Abfrage rein
            //_restApi = new RestMockService();
            _restApi = _restApi ?? new RestApiService();
            //_restApi = _restApi ?? new RestRealService();

            //Wenn ich das hier erstelle, dann wird ja für jeden View, der von BaseVM erbt ein neuer Gerätemanager angelegt. Nicht gut!
            // Durch die Umstellung auf static und die ?? Abfrage wird jetzt nur noch ein Object erstellt.
            _geraeteManager = _geraeteManager ?? new GeraeteManager(_restApi);

            //Wenn ich das hier mache, dann werden bei jedem aktiven Objekt die Events abgefangen und OnLoggedOUt ausgeführt, was dann zu mehreren Logout Views führt. Man muss das so gestalten, dass nur das aktive ViewModel/die aktive View das Event bekommt oder zur LoginView navigiert
             //App.LoginManager.LoggedOut += OnLoggedOut;
        }

        //protected virtual void OnLoggedOut(object sender, EventArgs e)
        //{
        //    if(_navigation.ModalStack.Count == 0)
        //    {
        //        _navigation.PushModalAsync(new LoginView());
        //    }
        //}

        #region INotifyPropertyChanges Handler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        #endregion

    }

}
