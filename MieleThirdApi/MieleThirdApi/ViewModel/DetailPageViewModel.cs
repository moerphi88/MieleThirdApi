using MieleThirdApi.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using MieleThirdApi.Model;

namespace MieleThirdApi.ViewModel
{
    class DetailPageViewModel : BaseViewModel
    {
        public DetailPageViewModel(INavigation navigation, Appliance details) : base(navigation)
        {
            // UpdateCommand = new Command(async () => await GetDeviceList());
            if (null != details)
            {
                Details.Ident = details.Ident.type.value_localized;
                Details.State = details.State.status.value_localized;
            } else
            {
                Details = new Model.Device() { Ident = "Schwein", State = "Hunhn" };            
            }
        }

        private Model.Device _details;
        public Model.Device Details
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

        private bool _pollingIsActive = true;

        private Model.Device _device;
        public Model.Device Dev
        {
            get { return _device; }
            set { _device = value; OnPropertyChanged(); }
        }

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

        public ICommand UpdateCommand { get; set; }

        async Task GetDeviceList()
        {
            IsBusy = true;
            var device = await _restApi.GetDeviceAsync();
            if (device != null) Dev = device; 
            IsBusy = false;
        }
    }
}
