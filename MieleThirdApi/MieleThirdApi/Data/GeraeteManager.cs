using System;
using System.Collections.Generic;
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
