using MieleThirdApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MieleThirdApi.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            StartTimer();
        }

        public string Titel { get; set; } = "MainPage";

        private int _count = 0;
        public string Count
        {
            get { return _count.ToString(); }
            set
            {                
                int.TryParse(value, out _count);
                OnPropertyChanged();
            }
        }

        // https://xamarinhelp.com/xamarin-forms-timer/
        void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                _count++;
                Count = _count.ToString();

                return true; // True = Repeat again, False = Stop the timer
            });
        }
    }
}
