using MieleThirdApi.ViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using MieleThirdApi.Model;
using System.Diagnostics;

namespace MieleThirdApi.ViewModel
{
    class DetailPageViewModel : BaseViewModel
    {
        Stopwatch watch = new Stopwatch();
        public DetailPageViewModel(INavigation navigation, String fabNr) : base(navigation, null)
        {
            watch.Start();
            Init(fabNr);
            
            BackNavigationCommand =  new Command(async () => await NavigateBack());
            
            System.Diagnostics.Debug.WriteLine($"Konstruktor fertig nach {watch.ElapsedMilliseconds} ms");

        }

        public DetailPageViewModel(INavigation navigation, DevicelistItem d) : base(navigation, null)
        {
            Details = d;
        }

        async Task NavigateBack()
        {
            System.Diagnostics.Debug.WriteLine("Button Klick");
            await _navigation.PopModalAsync();
        }

        async void Init(string fabNr)
        {
            IsBusy = true;
            var device = await _geraeteManager.GetDeviceAsync(fabNr);
            //Details = device as Model.Device;
            Details = new DevicelistItem { Name = "Hu", EndeZeit = "Fu" };
            IsBusy = false;
            watch.Stop();
            System.Diagnostics.Debug.WriteLine($"Init fertig nach {watch.ElapsedMilliseconds} ms");
        }

        private DevicelistItem _details;
        public DevicelistItem Details
        {
            get
            {
                return _details;
            }
            set
            {
                _details = value;
                OnPropertyChanged();
            }
        }

        //private bool _pollingIsActive = true;

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        // https://xamarinhelp.com/xamarin-forms-timer/
        //void StartPolling()
        //{
        //    Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        //    {
        //        _count++;
        //        Count = _count.ToString();
        //        GetDeviceList(); //Führt er das hier denn nu eigentlich auf dem MainThread aus? Oder Macht der einen Thread auf und arbeitet das ab, so wie es sollte

        //        return _pollingIsActive; // True = Repeat again, False = Stop the timer
        //    });
        //}

        public ICommand BackNavigationCommand { get; }

        //async Task GetDeviceList()
        //{
        //    IsBusy = true;
        //    var device = await _restApi.GetDeviceAsync();
        //    if (device != null) Dev = device; 
        //    IsBusy = false;
        //}
    }
}
