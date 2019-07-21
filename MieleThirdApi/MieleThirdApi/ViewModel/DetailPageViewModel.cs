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
        static int count = 0;
        public DetailViewCellModel Program { get; set; }
        public DetailPageViewModel(INavigation navigation, String fabNr) : base(navigation)
        {
            
            Init(fabNr);

            Program = new DetailViewCellModel();

            BackNavigationCommand =  new Command(async () => await NavigateBack());
            
            //System.Diagnostics.Debug.WriteLine($"{nameof(DetailPageViewModel)} Konstruktor fertig nach {App.watch.ElapsedMilliseconds} ms");
            //System.Diagnostics.Debug.WriteLine($"{nameof(DetailPageViewModel)} No. {count}");
            count++;

            //waitAndLogout();

        }

        //async Task waitAndLogout()
        //{
        //    await Task.Delay(3000);
        //    await App.LoginManager.Logout();
        //}

        ~DetailPageViewModel()
        {
            count--;
            System.Diagnostics.Debug.WriteLine($"{nameof(DetailPageViewModel)} Destruktor No. {count}");
        }

        public DetailPageViewModel(INavigation navigation, DevicelistItem d) : base(navigation)
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
            
            Program.KeyText = device.State.programType.key_localized.ToUpper();
            Program.ValueText = String.IsNullOrEmpty(device.State.programType.value_localized) ? "QuickPowerWash" : device.State.programType.value_localized;
            Program.ImageSource = "ic_program_generic_default.png";
            Program.IsEditable = true;

            IsBusy = false;
            System.Diagnostics.Debug.WriteLine($"Init fertig nach {App.watch.ElapsedMilliseconds} ms");
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
