using MieleThirdApi.ViewModels;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using MieleThirdApi.Model;
using MieleThirdApi.View;

namespace MieleThirdApi.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            StartPolling();
            UpdateCommand = new Command(async () => await GetDeviceList());
            NavigateCommand = new Command(async () => await NavigateToDetailPageAsync(null));

        }
        

        private bool _pollingIsActive = true;

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        private object _itemSelected;
        public object ItemSelected
        {
            get
            {
                return _itemSelected;
            }
            set
            {
                if (value != _itemSelected)
                {
                    _itemSelected = value;
                    OnPropertyChanged();
                    if(value != null)
                    {
                        //An dieser Stelle, könnte ich auch die Funktion aufrufen
                        // Anscheinend funktioniert die Parameterübertragung nur, wenn man ein COmmand bindet und dann per CommandParameter einen Parameter übergibt. Um es im Code zu lösen habe ich dazu keine Lösung gefunden
                        NavigateCommand.Execute(new object());
                        //NavigateToDetailPageAsync(_itemSelected as Appliance);
                        //ItemSelected = null;
                    }
                }
            }
        }

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
        void StartPolling()
        {
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                _count++;
                Count = _count.ToString();
                GetDeviceList(); //Führt er das hier denn nu eigentlich auf dem MainThread aus? Oder Macht der einen Thread auf und arbeitet das ab, so wie es sollte

                return _pollingIsActive; // True = Repeat again, False = Stop the timer
            });
        }

        async Task NavigateToDetailPageAsync(Appliance details)
        {
            _pollingIsActive = false;
            await _navigation.PushModalAsync(new DetailPage(details));
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand NavigateCommand { get; set; }
        //{
        //    get
        //    {
        //        return new Command<Appliance>(async (x) => await NavigateToDetailPageAsync(x));
        //    }
        //}

        async Task GetDeviceList()
        {
            IsBusy = true;
            var devices = await _restApi.GetDevicesListAsync();
            if (devices != null) DeviceList = new ObservableCollection<Model.Device>(devices);
            IsBusy = false;
        }
    }
}
