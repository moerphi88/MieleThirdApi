using MieleThirdApi.ViewModel;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using MieleThirdApi.Model;
using MieleThirdApi.View;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Xamarin.Essentials;

namespace MieleThirdApi.ViewModel
{
    class MainPageViewModel : BaseViewModel
    {        
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            _geraeteManager.StartPolling();

            _geraeteManager.DeviceListUpdated += UpdateObservableCollectionDeviceList;
            //GetDeviceList();
            UpdateCommand = new Command(async () => await GetDeviceList());
            NavigateCommand = new Command(async () => await NavigateToDetailPageAsync());

            DeviceList = new ObservableCollection<DevicelistItem>();
        }

        private void UpdateObservableCollectionDeviceList(object sender, DeviceListEventArgs e)
        {
            // An dieser stelle die Liste entgegennehmen und meine ObservableCOllection updaten
            // Was ist der beste Weg? Alles austauschen oder alte weg und neue hnzufügen? Oder einzelne Parameter anüassen?
            Debug.WriteLine($"{nameof(UpdateObservableCollectionDeviceList)} has been called from {sender.ToString()} and it has send {e.DevicelistItemList[0].Name}");
        }

        //private bool _pollingIsActive = true;
        private string _fabNr = "12345678";
        private ObservableCollection<DevicelistItem> _deviceList;
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
                        _fabNr = (ItemSelected as DevicelistItem).FabNr;

                        NavigateCommand.Execute(ItemSelected);

                        ItemSelected = null; // Ich darf es nicht wieder zurücksetzen, wenn ich dieses Element übergebe, aber ich könnte hier eine Kopie anlegen oder aber gleich das wichtigste herusfiltern und übertragen?! Die FabNr, die ich zum pollen brauche
                    }
                }
            }
        }


        public ObservableCollection<DevicelistItem> DeviceList
        {
            get { return _deviceList; }
            set { _deviceList = value; OnPropertyChanged(); }
        }

        // https://xamarinhelp.com/xamarin-forms-timer/
        //void StartPolling()
        //{
        //    Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(3), () =>
        //    {
        //        try
        //        {
        //            Task.Run(async () =>
        //            {
        //                await GetDeviceList();
        //                DeviceList.First(d => d.FabNr == "000124430017").Name += 1;
        //            }); //Führt er das hier denn nu eigentlich auf dem MainThread aus? Oder Macht der einen Thread auf und arbeitet das ab, so wie es sollte

        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine($"Exception in {nameof(StartPolling)} : {ex.Message}");
        //        }
        //        return true;
        //        //return _pollingIsActive; // True = Repeat again, False = Stop the timer
        //    });
        //}

        async Task NavigateToDetailPageAsync()
        {
            IsBusy = true;
            //System.Diagnostics.Debug.WriteLine($"NavigateToDetailPageAsync is called {App.watch.ElapsedMilliseconds} ms");
            //_pollingIsActive = false;
            // Fake loading / wait to overrule the bad loading/behavior of the list view selection and detail Page loading
            var detailPage = new DetailPage(_fabNr);
            await Task.Delay(700);
            await _navigation.PushAsync(detailPage);
            //System.Diagnostics.Debug.WriteLine($"PushAsync for DetailPage done at  {App.watch.ElapsedMilliseconds} ms");
            IsBusy = false;
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand NavigateCommand { get; set; }

        public async Task GetDeviceList()
        {
            IsBusy = true;
            var devicelistItems = await _geraeteManager.GetDevicelistItemsAsync();
            //TODO die Konvertierung der Model muss auch noch gemacht werden
            //TODO An dieser Stelle gucken, ob neue Items hinzugekommen sind. Vergleich der FabNr?! (Linq?)
            //Ist an dieser Stelle if(DeviceList == null) ... besser oder DeviceList = Devicelist ?? new Obs.... TODO Zeiten messen
            DeviceList = DeviceList ?? new ObservableCollection<DevicelistItem>();
            if (devicelistItems != null)
            {
                if(DeviceList.Count == 0) DeviceList = new ObservableCollection<DevicelistItem>(devicelistItems);
                foreach (var d in devicelistItems)
                {
                //    //DeviceList.Add(d);

                    if(!(DeviceList.Any(x => x.FabNr == d.FabNr)))
                        DeviceList.Add(d);
                    //    //MsgList.Where(x => !SentList.Any(y => y.MsgID == x.MsgID))
                }
                //An dieser stelle die Kompletten Objekte vergleichen, ob etwas unterschiedlich ist und nur dann aktualisieren? Idee: einen Hash anlegen und den vergleichen?!
                // Dann muss aber die Progressbar und alles, was sich viel verändert raus? Oder halt nciht. FÜr die Fälle wenn es etwas läuft muss man halt etwas mehr gucken
                // Vielleicht ist ein Vergleich über Equals dann doch noch einfacher?
                //var list = DeviceList.Where(x => devicelistItems.Any(y => y.FabNr == x.FabNr));
            }
            //if (devices != null) DeviceList = new ObservableCollection<DevicelistItem>(list);
            IsBusy = false;
        }
    }
}
