using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MieleThirdApi.Model;

namespace MieleThirdApi.Data
{
    public class GeraeteManager : IGeraeteManager
    {
        private IRestApi _restApi;

        //For Chaching puposes
        private Appliance _cachedAppliance; //Todo this must be a dictionary<fabNr, Appliance>
        private List<Appliance> _cachedListOfAppliance;

        public GeraeteManager(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public void StopPolling()
        {
            _pollingIsActive = false;
            //TODO einen angefangenen Task müsste ich auch noch abbrechen. Cancelation Token
        }

        private bool _pollingIsActive = true;
        public void StartPolling()
        {
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                try
                {
                    Task.Run(async () =>
                    {
                        var list = await GetDevicelistItemsAsync();
                        //await Task.Delay(2000);
                        // So erweitern, dass hier geschaut wird, ob sich etwas geändert hat
                        DeviceListUpdated?.Invoke(this, new DeviceListEventArgs() { DevicelistItemList = list});
                    }); //Führt er das hier denn nu eigentlich auf dem MainThread aus? Oder Macht der einen Thread auf und arbeitet das ab, so wie es sollte

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception in {nameof(StartPolling)} : {ex.Message}");
                }
                return _pollingIsActive; // True = Repeat again, False = Stop the timer
            });
        }

        public event EventHandler<DeviceListEventArgs> DeviceListUpdated;


        public async Task<Appliance> GetDeviceAsync(string fabNr)
        {   
            var appliance = await _restApi.GetApplianceAsync(fabNr);
            if(appliance != null)
            {
                return _cachedAppliance = appliance;
            } else
            {
                //TODO: In case the first call misses, I need to send an empty Appliance. 
                return _cachedAppliance ?? new Appliance();
            }
        }

        public async Task<List<DevicelistItem>> GetDevicelistItemsAsync()
        {
            var listOfAppliances = await _restApi.GetAppliancesListAsync();
            if(listOfAppliances != null)
            {
                _cachedListOfAppliance = listOfAppliances;
                return CreateDevicelistItemListFromAppliances(listOfAppliances);
            } else
            {                
                return CreateDevicelistItemListFromAppliances(_cachedListOfAppliance ?? new List<Appliance>());
            }            
        }

        private List<DevicelistItem> CreateDevicelistItemListFromAppliances(List<Appliance> listOfAppliances)
        {
            //TODO an dieser Stelle über listOfAppliances iterieren und neue DevielistItems erstellen
            var list = new List<DevicelistItem>();
            foreach(var a in listOfAppliances)
            {
                list.Add(new DevicelistItem(a));
            }
            //list.Add(new DevicelistItem());
            //list.Add(new DevicelistItem() { ProgressBarValue = 0 });
            //list.Add(new DevicelistItem() { EndeZeit = string.Empty });
            //list.Add(new DevicelistItem() { ProgressBarValue = .9, EndeZeit = string.Empty, Status = "Running" });
            //list.Add(new DevicelistItem() { ProgressBarValue = 0.4 });
            //list.Add(new DevicelistItem() { ProgressBarValue = 0.2 });
            //list.Add(new DevicelistItem() { ProgressBarValue = 0.99 });
            //list.Add(new DevicelistItem() { ProgressBarValue = .9, EndeZeit = string.Empty, Status = "Running" });
            //list.Add(new DevicelistItem() { ProgressBarValue = 0.4 });
            //list.Add(new DevicelistItem() { ProgressBarValue = 0.2 });
            //list.Add(new DevicelistItem() { ProgressBarValue = 0.99 });

            return list;
        }


            

    }
}
