using MieleThirdApi.Data;
using MieleThirdApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MieleThirdApi.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(this.Navigation);
        }

        protected override bool OnBackButtonPressed()
        {
            //now the app goes to background, if the user presses the button
            base.OnBackButtonPressed();
            return false;
        }
    }
}