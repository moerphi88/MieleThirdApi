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

        public GeraeteManager()
        {
            //Hier kann später die Compiler MOCK Abfrage rein
            //_restApi = new RestMockService();
            _restApi = new RestApiService();
        }

        public Task<Appliance> GetDeviceAsync(string fabNr)
        {
            return _restApi.GetDeviceAsync(fabNr);
        }

        public async Task<List<DevicelistItem>> GetDevicelistItemsAsync()
        {
            var listOfAppliances = await _restApi.GetDevicesListAsync();
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
            //list.Add(new DevicelistItem() { ProgressBarValue = 0 });
            //list.Add(new DevicelistItem() { EndeZeit = string.Empty });
            //list.Add(new DevicelistItem() { ProgressBarValue=.9, EndeZeit = string.Empty, Status = "Running" });

            return list;
        }


            

    }
}
