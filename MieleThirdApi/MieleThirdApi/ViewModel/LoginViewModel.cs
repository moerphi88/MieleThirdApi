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
        

        public Credential Credential
        {
            get { return _credential; }
            set { _credential = value; OnPropertyChanged(); }
        }
        public LoginViewModel(INavigation navigation, ILoginManager loginManager) : base(navigation)
        {
            LoginCommand = new Command(async () => await LoginAsync());
        }

        async Task LoginAsync()
        {
            await _navigation.PushModalAsync(new MainPage());
        }

        public ICommand LoginCommand { get; set; }
    }
}
