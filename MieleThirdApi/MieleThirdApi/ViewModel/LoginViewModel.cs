using MieleThirdApi.Data;
using MieleThirdApi.Model;
using MieleThirdApi.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MieleThirdApi.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        private Credential _credential;
        private ILoginManager _loginManager;

        public Credential Credential
        {
            get { return _credential; }
            set { _credential = value; OnPropertyChanged(); }
        }
        public LoginViewModel(INavigation navigation) : base(navigation)
        {
            _loginManager = App.LoginManager;
            Credential = new Credential();

            LoginCommand = new Command(async () => await LoginAsyncCommand());
        }

        async Task LoginAsyncCommand()
        {
            IsBusy = true;
            var success = await _loginManager.LoginAsync(Credential);
            IsBusy = false;
            if (success)
            {                
                await _navigation.PopModalAsync();
            }
            else
            {                
                await App.Current.MainPage.DisplayAlert("Halt Stop!", "Der Login war nicht Erfolgreich", "Ok");
            }
        }

        public ICommand LoginCommand { get; set; }
    }
}
