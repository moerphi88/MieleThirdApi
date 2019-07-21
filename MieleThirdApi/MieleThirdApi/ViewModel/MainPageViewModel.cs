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
        
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            //StartPolling();
            GetDeviceList();
            UpdateCommand = new Command(async () => await GetDeviceList());
            NavigateCommand = new Command(async () => await NavigateToDetailPageAsync());

            //Prepare a DetailPage, so that I can navigate to it afterwards
            //detailPage = new DetailPage(_fabNr);
            //System.Diagnostics.Debug.WriteLine($"{nameof(MainPageViewModel)} Konstruktor fertig nach {App.watch.ElapsedMilliseconds} ms");
        }       

        private bool _pollingIsActive = true;
        private string _fabNr = "12345678";
        private DetailPage detailPage;

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
                        //Anscheinend funktioniert die Parameterübertragung nur, wenn man ein COmmand bindet und dann per CommandParameter einen Parameter übergibt. Um es im Code zu lösen habe ich dazu keine Lösung gefunden
                        //System.Diagnostics.Debug.WriteLine($"ItemSelected OnPropertyChanged {App.watch.ElapsedMilliseconds} ms");
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
            IsBusy = true;
            //System.Diagnostics.Debug.WriteLine($"NavigateToDetailPageAsync is called {App.watch.ElapsedMilliseconds} ms");
            _pollingIsActive = false;
            // Fake loading / wait to overrule the bad loading/behavior of the list view selection and detail Page loading
            var detailPage = new DetailPage(_fabNr);
            await Task.Delay(700);
            await _navigation.PushAsync(detailPage);
            //System.Diagnostics.Debug.WriteLine($"PushAsync for DetailPage done at  {App.watch.ElapsedMilliseconds} ms");
            IsBusy = false;
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
