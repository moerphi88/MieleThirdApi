using MieleThirdApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    //TODO diese Klasse muss ich noch komplett neu mocken! oO
    public class RestMockService : IRestApi
    {
        public async Task<Appliance> GetApplianceAsync(string fabNr)
        {
            await Task.Delay(3000);
            return new Appliance();
        }

        public async Task<List<Appliance>> GetApplincesListAsync()
        {
            var appliances = new List<Appliance>();
            await Task.Delay(3000);

            //devices.Add(new Device() { State = "State1", Ident = "Ident1" });
            //devices.Add(new Device() { State = "State2", Ident = "Ident2" });
            //devices.Add(new Device() { State = "State3", Ident = "Ident3" });
            appliances.Add(new Appliance());
            appliances.Add(new Appliance());

            return appliances;
        }
    }
}
