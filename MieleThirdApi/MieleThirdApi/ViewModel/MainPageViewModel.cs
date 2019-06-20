using MieleThirdApi.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using MieleThirdApi.Model;

namespace MieleThirdApi.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            StartTimer();
            UpdateCommand = new Command(async () => await GetDeviceList());

            DeviceList = new ObservableCollection<Model.Device>();
            DeviceList.Add(new Model.Device() { State = "Hans", Ident = "Franz" });
            DeviceList.Add(new Model.Device() { State = "Hans", Ident = "Franz" });
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public string Titel { get; set; } = "MainPage";

        private ObservableCollection<Model.Device> _deviceList;
        public ObservableCollection<Model.Device> DeviceList
        {
            get { return _deviceList; }
            set { _deviceList = value; OnPropertyChanged(); }
        }

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
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                _count++;
                Count = _count.ToString();

                return true; // True = Repeat again, False = Stop the timer
            });
        }

        public ICommand UpdateCommand { get; set; }

        async Task GetDeviceList()
        {
            IsBusy = true;
            var devices = await _restApi.GetDevicesListAsync();
            if (devices != null) DeviceList = new ObservableCollection<Model.Device>(devices);
            IsBusy = false;
        }
    }
}
