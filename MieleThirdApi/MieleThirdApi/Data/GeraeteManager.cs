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

        public GeraeteManager(IRestApi restApi)
        {
            _restApi = restApi;
        }

        public Task<Appliance> GetDeviceAsync(string fabNr)
        {
            return _restApi.GetApplianceAsync(fabNr);
        }

        public async Task<List<DevicelistItem>> GetDevicelistItemsAsync()
        {
            var listOfAppliances = await _restApi.GetApplincesListAsync();
            //Here the magic model conversion must happen
            return CreateDevicelistItemListFromAppliances(listOfAppliances);
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
            list.Add(new DevicelistItem() { ProgressBarValue = 0 });
            list.Add(new DevicelistItem() { EndeZeit = string.Empty });
            list.Add(new DevicelistItem() { ProgressBarValue=.9, EndeZeit = string.Empty, Status = "Running" });
            list.Add(new DevicelistItem() { ProgressBarValue = 0.4 });
            list.Add(new DevicelistItem() { ProgressBarValue = 0.2 });
            list.Add(new DevicelistItem() { ProgressBarValue = 0.99 });

            return list;
        }


            

    }
}
