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
            //https://iwritecodesometimes.net/2018/06/25/building-a-next-entry-effect-for-ios-and-android-in-xamarin-forms/
            Entry_First.ReturnCommand = new Command(() => Entry_Second.Focus());
        }

        protected override bool OnBackButtonPressed()
        {
            //now the app goes to background, if the user presses the button
            base.OnBackButtonPressed();
            return false;
        }
    }
}