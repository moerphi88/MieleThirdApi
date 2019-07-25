using MieleThirdApi.Data;
using MieleThirdApi.View;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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

            //Debug.WriteLine("Sowieso");
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
            Debug.WriteLine("OnStart");

            //AppCenter.Start("android=6144b737-72ba-4f7a-9abe-dc75cf54ab6e;" +
            //      "uwp={Your UWP App secret here};" +
            //      "ios={Your iOS App secret here}",
            //      typeof(Analytics), typeof(Crashes));

            AppCenter.Start("android=6144b737-72ba-4f7a-9abe-dc75cf54ab6e;" +
                "ios=14f0918d-41d0-46cb-84e5-cf16b985611d;",
                  typeof(Analytics), typeof(Crashes));

            AppCenter.LogLevel = LogLevel.Verbose;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Debug.WriteLine("OnSleep");
            watch.Stop();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("OnResume");
        }
    }
}
