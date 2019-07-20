using MieleThirdApi.Data;
using MieleThirdApi.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MieleThirdApi
{
    public partial class App : Application
    {
        public static ILoginManager LoginManager { get; private set; }
        public App()
        {
            InitializeComponent();

            LoginManager = LoginManager ?? new LoginMockManager();
                                   
            MainPage = new NavigationPage( new MainPage());
            if (!LoginManager.IsLoggedIn())
            {
                MainPage.Navigation.PushModalAsync(new LoginView());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
