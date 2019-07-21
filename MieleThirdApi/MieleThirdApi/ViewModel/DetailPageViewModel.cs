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
        public DetailViewCellModel Program { get; set; }
        public DetailPageViewModel(INavigation navigation, String fabNr) : base(navigation)
        {            
            Init(fabNr);
            Program = new DetailViewCellModel();
            BackNavigationCommand =  new Command(async () => await NavigateBack());
            Title = "Waschmaschine";
        }

        public DetailPageViewModel(INavigation navigation) : base(navigation)
        {
            Title = "Design";
            Program = new DetailViewCellModel()
            {
                KeyText = "Design",
                ValueText = "Design",
                ImageSource = "ic_program_generic_default.png",
                IsEditable = true
            };
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        async Task NavigateBack()
        {
            System.Diagnostics.Debug.WriteLine("Button Klick");
            await _navigation.PopModalAsync();
        }

        async void Init(string fabNr)
        {
            IsBusy = true;
            var appliance = await _geraeteManager.GetDeviceAsync(fabNr);
            //Details = device as Model.Device;
            Details = new DevicelistItem { Name = "Hu", EndeZeit = "Fu" };
            if(appliance.State != null)
            {
                Program.KeyText = appliance.State.programType.key_localized.ToUpper();
                Program.ValueText = String.IsNullOrEmpty(appliance.State.programType.value_localized) ? "QuickPowerWash" : appliance.State.programType.value_localized;
                Program.ImageSource = "ic_program_generic_default.png";
                Program.IsEditable = true;
            } else
            {
                Program.KeyText = "Error nothing loaded!";
                Program.ValueText = "Error nothing loaded!";
                Program.ImageSource = "ic_program_generic_default.png";
                Program.IsEditable = true;
            }

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
