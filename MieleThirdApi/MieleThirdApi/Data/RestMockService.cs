using MieleThirdApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    public class RestMockService : IRestApi
    {
        public async Task<Device> GetDeviceAsync()
        {
            await Task.Delay(1000);
            return new Device() { State = "State1", Ident = "Ident1" };
        }

        public async Task<List<Device>> GetDevicesListAsync()
        {
            var devices = new List<Device>();
            await Task.Delay(1000);

            devices.Add(new Device() { State = "State1", Ident = "Ident1" });
            devices.Add(new Device() { State = "State2", Ident = "Ident2" });
            devices.Add(new Device() { State = "State3", Ident = "Ident3" });

            return devices;
        }
    }
}
