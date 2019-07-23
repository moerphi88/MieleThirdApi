using MieleThirdApi.Data;
using MieleThirdApi.View;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MieleThirdApi
{
    public partial class App : Application
    {
        public static ILoginManager LoginManager { get; private set; }
        public static Stopwatch watch = new Stopwatch();
        public App()
        {
            watch.Start();
            //System.Diagnostics.Debug.WriteLine($"Zeitmessung {App.watch.ElapsedMilliseconds} ms");
            InitializeComponent();

            Debug.WriteLine("Sowieso");
            LoginManager = LoginManager ?? new LoginMockManager();
            //(LoginManager = LoginManager ?? new LoginManager();

            var navBar = new NavigationPage(new MainPage());
            MainPage = navBar;
            navBar.BarBackgroundColor = Color.FromHex("#1F2328");
            navBar.BarTextColor = Color.FromHex("#a1a1a1");
            
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
            watch.Stop();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
