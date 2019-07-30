using MieleThirdApi.Data;
using MieleThirdApi.Model;
using MieleThirdApi.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MieleThirdApi.ViewModel
{
    class SettingsPageViewModel : BaseViewModel
    {
        private ILoginManager _loginManager;
        public SettingsPageViewModel(INavigation navigation) : base(navigation)
        {
            _loginManager = App.LoginManager;
            
            LogoutCommand = new Command( async () => await LogoutAsync());
        }

        private async Task LogoutAsync()
        {
            _loginManager.Logout();
            await _navigation.PushModalAsync(new LoginView());
            await _navigation.PopAsync(false);
        }

        public ICommand LogoutCommand { get; set; }
    }
}
