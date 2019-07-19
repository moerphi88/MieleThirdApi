using MieleThirdApi.ViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using MieleThirdApi.Model;
using MieleThirdApi.View;
using System.Collections.Generic;

namespace MieleThirdApi.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigation navigation) : base(navigation, null)
        {
            //StartPolling();
            GetDeviceList();
            UpdateCommand = new Command(async () => await GetDeviceList());
            NavigateCommand = new Command(async () => await NavigateToDetailPageAsync());
        }       

        private bool _pollingIsActive = true;
        private string _fabNr = "12345678";

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

                        _fabNr = ItemSelected.ToString();
                        NavigateCommand.Execute(ItemSelected);
                        
                        ItemSelected = null; // Ich darf es nicht wieder zurücksetzen, wenn ich dieses Element übergebe, aber ich könnte hier eine Kopie anlegen oder aber gleich das wichtigste herusfiltern und übertragen?! Die FabNr, die ich zum pollen brauche
                    }
                }
            }
        }

        private ObservableCollection<DevicelistItem> _deviceList;
        public ObservableCollection<DevicelistItem> DeviceList
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

        async Task NavigateToDetailPageAsync()
        {
            _pollingIsActive = false;
            await _navigation.PushModalAsync(new DetailPage(_fabNr));
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        async Task GetDeviceList()
        {
            IsBusy = true;
            var devicelistItems = await _geraeteManager.GetDevicelistItemsAsync();
            //TODO die Konvertierung der Model muss auch noch gemacht werden
            
            if (devicelistItems != null) DeviceList = new ObservableCollection<DevicelistItem>(devicelistItems);
            //if (devices != null) DeviceList = new ObservableCollection<DevicelistItem>(list);
            IsBusy = false;
        }
    }
}
