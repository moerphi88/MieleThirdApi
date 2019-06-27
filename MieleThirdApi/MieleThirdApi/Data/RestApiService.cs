using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MieleThirdApi.Model;
using Newtonsoft.Json;

namespace MieleThirdApi.Data
{
    public class RestApiService : IRestApi
    {

        public async Task<List<Appliance>> GetDevicesListAsync()
        {
            //var stringResponse = "{\"ident\":{\"type\":{\"key_localized\":\"Devicetype\",\"value_raw\":18,\"value_localized\":\"VentilationHood\"},\"deviceName\":\"MyHood\",\"deviceIdentLabel\":{\"fabNumber\":\"000124430017\",\"fabIndex\":\"00\",\"techType\":\"DA-6996\",\"matNumber\":\"10101010\",\"swids\":[\"4164\",\"20380\",\"25226\"]},\"xkmIdentLabel\":{\"techType\":\"EK039W\",\"releaseVersion\":\"02.31\"}},\"state\":{\"status\":{\"value_raw\":5,\"value_localized\":\"Inuse\",\"key_localized\":\"State\"},\"programType\":{\"value_raw\":0,\"value_localized\":\"\",\"key_localized\":\"Programme\"},\"programPhase\":{\"value_raw\":4609,\"value_localized\":\"\",\"key_localized\":\"Phase\"},\"remainingTime\":[0,0],\"startTime\":[0,0],\"targetTemperature\":{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},\"temperature\":[{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"}],\"signalInfo\":false,\"signalFailure\":false,\"signalDoor\":false,\"remoteEnable\":{\"fullRemoteControl\":false,\"smartGrid\":false},\"light\":1,\"elapsedTime\":[],\"dryingStep\":{\"value_raw\":null,\"value_localized\":\"\",\"key_localized\":\"Dryinglevel\"},\"ventilationStep\":{\"value_raw\":2,\"value_localized\":\"2\",\"key_localized\":\"PowerLevel\"}}}";

            var stringResponse = "{\"1234\":{\"ident\":{\"type\":{\"key_localized\":\"Devicetype\",\"value_raw\":18,\"value_localized\":\"VentilationHood\"},\"deviceName\":\"MyHood\",\"deviceIdentLabel\":{\"fabNumber\":\"000124430017\",\"fabIndex\":\"00\",\"techType\":\"DA-6996\",\"matNumber\":\"10101010\",\"swids\":[\"4164\",\"20380\",\"25226\"]},\"xkmIdentLabel\":{\"techType\":\"EK039W\",\"releaseVersion\":\"02.31\"}},\"state\":{\"status\":{\"value_raw\":5,\"value_localized\":\"Inuse\",\"key_localized\":\"State\"},\"programType\":{\"value_raw\":0,\"value_localized\":\"\",\"key_localized\":\"Programme\"},\"programPhase\":{\"value_raw\":4609,\"value_localized\":\"\",\"key_localized\":\"Phase\"},\"remainingTime\":[1,20],\"startTime\":[0,0],\"targetTemperature\":{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},\"temperature\":[{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"}],\"signalInfo\":false,\"signalFailure\":false,\"signalDoor\":false,\"remoteEnable\":{\"fullRemoteControl\":false,\"smartGrid\":false},\"light\":1,\"elapsedTime\":[],\"dryingStep\":{\"value_raw\":null,\"value_localized\":\"\",\"key_localized\":\"Dryinglevel\"},\"ventilationStep\":{\"value_raw\":2,\"value_localized\":\"2\",\"key_localized\":\"PowerLevel\"}}},\"12345\":{\"ident\":{\"type\":{\"key_localized\":\"Devicetype\",\"value_raw\":18,\"value_localized\":\"VentilationHood\"},\"deviceName\":\"\",\"deviceIdentLabel\":{\"fabNumber\":\"000124430017\",\"fabIndex\":\"00\",\"techType\":\"DA-6996\",\"matNumber\":\"10101010\",\"swids\":[\"4164\",\"20380\",\"25226\"]},\"xkmIdentLabel\":{\"techType\":\"EK039W\",\"releaseVersion\":\"02.31\"}},\"state\":{\"status\":{\"value_raw\":5,\"value_localized\":\"Inuse\",\"key_localized\":\"State\"},\"programType\":{\"value_raw\":0,\"value_localized\":\"\",\"key_localized\":\"Programme\"},\"programPhase\":{\"value_raw\":4609,\"value_localized\":\"\",\"key_localized\":\"Phase\"},\"remainingTime\":[0,32],\"startTime\":[0,0],\"targetTemperature\":{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},\"temperature\":[{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"}],\"signalInfo\":false,\"signalFailure\":false,\"signalDoor\":false,\"remoteEnable\":{\"fullRemoteControl\":false,\"smartGrid\":false},\"light\":1,\"elapsedTime\":[],\"dryingStep\":{\"value_raw\":null,\"value_localized\":\"\",\"key_localized\":\"Dryinglevel\"},\"ventilationStep\":{\"value_raw\":2,\"value_localized\":\"2\",\"key_localized\":\"PowerLevel\"}}}}";
            var appliance = await Task.Run(() => JsonConvert.DeserializeObject<Dictionary<string,Appliance>>(stringResponse));
            
            var list = new List<Appliance>();
            //TODO An dieser Stelle mit foreach über das dictionary gehen und eine neue Liste erzeugen
            list.Add(appliance["1234"]);
            list.Add(appliance["12345"]);
            //list.Add(new Device() { State = device["1234"].State.status.value_localized, Ident = device["1234"].Ident.type.value_localized });
            //list.Add(new Device() { State = device["12345"].State.status.value_localized, Ident = device["12345"].Ident.type.value_localized });

            return list;
        }

        public async Task<Appliance> GetDeviceAsync(string fabNr)
        {
            await Task.Delay(1000);

            var stringResponse = "{\"ident\":{\"type\":{\"key_localized\":\"Devicetype\",\"value_raw\":18,\"value_localized\":\"VentilationHood\"},\"deviceName\":\"MyHood\",\"deviceIdentLabel\":{\"fabNumber\":\"000124430017\",\"fabIndex\":\"00\",\"techType\":\"DA-6996\",\"matNumber\":\"10101010\",\"swids\":[\"4164\",\"20380\",\"25226\"]},\"xkmIdentLabel\":{\"techType\":\"EK039W\",\"releaseVersion\":\"02.31\"}},\"state\":{\"status\":{\"value_raw\":5,\"value_localized\":\"Inuse\",\"key_localized\":\"State\"},\"programType\":{\"value_raw\":0,\"value_localized\":\"\",\"key_localized\":\"Programme\"},\"programPhase\":{\"value_raw\":4609,\"value_localized\":\"\",\"key_localized\":\"Phase\"},\"remainingTime\":[0,0],\"startTime\":[0,0],\"targetTemperature\":{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},\"temperature\":[{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"}],\"signalInfo\":false,\"signalFailure\":false,\"signalDoor\":false,\"remoteEnable\":{\"fullRemoteControl\":false,\"smartGrid\":false},\"light\":1,\"elapsedTime\":[],\"dryingStep\":{\"value_raw\":null,\"value_localized\":\"\",\"key_localized\":\"Dryinglevel\"},\"ventilationStep\":{\"value_raw\":2,\"value_localized\":\"2\",\"key_localized\":\"PowerLevel\"}}}";
            var appliance = await Task.Run(() => JsonConvert.DeserializeObject<Appliance>(stringResponse));
            return appliance;
        }


    }
}
