using MieleThirdApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    public interface IGeraeteManager
    {
        Task<List<DevicelistItem>> GetDevicelistItemsAsync();
        Task<Appliance> GetDeviceAsync(string fabNr);
    }
}
