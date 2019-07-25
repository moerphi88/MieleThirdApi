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

            //Debug.WriteLine("Sowieso");
            LoginManager = LoginManager ?? new LoginMockManager();
            //(LoginManager = LoginManager ?? new LoginManager();
                        
            MainPage = new NavigationPage(new MainPage());
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#1F2328"));
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.FromHex("#a1a1a1"));

          //if ((!LoginManager.IsLoggedIn().Result))
          //  {
          //      MainPage.Navigation.PushModalAsync(new LoginView());
          //  }
        }


        protected override async void OnStart()
        {
            // Handle when your app starts
            if ((!(await LoginManager.IsLoggedIn()))) 
            {
                await MainPage.Navigation.PushModalAsync(new LoginView());
            }
            Debug.WriteLine("OnStart");
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
