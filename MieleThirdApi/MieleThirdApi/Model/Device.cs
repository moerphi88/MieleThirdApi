using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MieleThirdApi.Model
{
    // {\"ident\":{\"type\":{\"key_localized\":\"Devicetype\",\"value_raw\":18,\"value_localized\":\"VentilationHood\"},\"deviceName\":\"MyHood\",\"deviceIdentLabel\":{\"fabNumber\":\"000124430017\",\"fabIndex\":\"00\",\"techType\":\"DA-6996\",\"matNumber\":\"10101010\",\"swids\":[\"4164\",\"20380\",\"25226\"]},\"xkmIdentLabel\":{\"techType\":\"EK039W\",\"releaseVersion\":\"02.31\"}},\"state\":{\"status\":{\"value_raw\":5,\"value_localized\":\"Inuse\",\"key_localized\":\"State\"},\"programType\":{\"value_raw\":0,\"value_localized\":\"\",\"key_localized\":\"Programme\"},\"programPhase\":{\"value_raw\":4609,\"value_localized\":\"\",\"key_localized\":\"Phase\"},\"remainingTime\":[0,0],\"startTime\":[0,0],\"targetTemperature\":{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},\"temperature\":[{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"},{\"value_raw\":-32768,\"value_localized\":null,\"unit\":\"Celsius\"}],\"signalInfo\":false,\"signalFailure\":false,\"signalDoor\":false,\"remoteEnable\":{\"fullRemoteControl\":false,\"smartGrid\":false},\"light\":1,\"elapsedTime\":[],\"dryingStep\":{\"value_raw\":null,\"value_localized\":\"\",\"key_localized\":\"Dryinglevel\"},\"ventilationStep\":{\"value_raw\":2,\"value_localized\":\"2\",\"key_localized\":\"PowerLevel\"}}}
    /*
     * {
    "ident": {
      "type": {
        "key_localized": "Devicetype",
        "value_raw": 18,
        "value_localized": "Ventilation Hood"
      },
      "deviceName": "My Hood",
      "deviceIdentLabel": {
        "fabNumber": "000124430017",
        "fabIndex": "00",
        "techType": "DA-6996",
        "matNumber": "10101010",
        "swids": [
          "4164",
          "20380",
          "25226"
        ]
      },
      "xkmIdentLabel": {
        "techType": "EK039W",
        "releaseVersion": "02.31"
      }
    },
    "state": {
      "status": {
        "value_raw": 5,
        "value_localized": "In use",
        "key_localized": "State"
      },
      "programType": {
        "value_raw": 0,
        "value_localized": "",
        "key_localized": "Programme"
      },
      "programPhase": {
        "value_raw": 4609,
        "value_localized": "",
        "key_localized": "Phase"
      },
      "remainingTime": [
        0,
        0
      ],
      "startTime": [
        0,
        0
      ],
      "targetTemperature": {
        "value_raw": -32768,
        "value_localized": null,
        "unit": "Celsius"
      },
      "temperature": [
        {
          "value_raw": -32768,
          "value_localized": null,
          "unit": "Celsius"
        },
        {
          "value_raw": -32768,
          "value_localized": null,
          "unit": "Celsius"
        },
        {
          "value_raw": -32768,
          "value_localized": null,
          "unit": "Celsius"
        }
      ],
      "signalInfo": false,
      "signalFailure": false,
      "signalDoor": false,
      "remoteEnable": {
        "fullRemoteControl": false,
        "smartGrid": false
      },
      "light": 1,
      "elapsedTime": [],
      "dryingStep": {
        "value_raw": null,
        "value_localized": "",
        "key_localized": "Drying level"
      },
      "ventilationStep": {
        "value_raw": 2,
        "value_localized": "2",
        "key_localized": "Power Level"
      }
    }
  }
     * 
     */

    public class Device
    {
        [JsonProperty("ident")]
        public string Ident { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }

    public class Appliance
    {
        [JsonProperty("ident")]
        public Ident Ident { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }
    }

    public class Type
    {
        public string key_localized { get; set; }
        public int value_raw { get; set; }
        public string value_localized { get; set; }
    }

    public class DeviceIdentLabel
    {
        public string fabNumber { get; set; }
        public string fabIndex { get; set; }
        public string techType { get; set; }
        public string matNumber { get; set; }
        public List<string> swids { get; set; }
    }

    public class XkmIdentLabel
    {
        public string techType { get; set; }
        public string releaseVersion { get; set; }
    }

    public class Ident
    {
        public Type type { get; set; }
        public string deviceName { get; set; }
        public DeviceIdentLabel deviceIdentLabel { get; set; }
        public XkmIdentLabel xkmIdentLabel { get; set; }
    }

    public class Status
    {
        public int value_raw { get; set; }
        public string value_localized { get; set; }
        public string key_localized { get; set; }
    }

    public class ProgramType
    {
        public int value_raw { get; set; }
        public string value_localized { get; set; }
        public string key_localized { get; set; }
    }

    public class ProgramPhase
    {
        public int value_raw { get; set; }
        public string value_localized { get; set; }
        public string key_localized { get; set; }
    }

    public class TargetTemperature
    {
        public int value_raw { get; set; }
        public object value_localized { get; set; }
        public string unit { get; set; }
    }

    public class Temperature
    {
        public int value_raw { get; set; }
        public object value_localized { get; set; }
        public string unit { get; set; }
    }

    public class RemoteEnable
    {
        public bool fullRemoteControl { get; set; }
        public bool smartGrid { get; set; }
    }

    public class DryingStep
    {
        public object value_raw { get; set; }
        public string value_localized { get; set; }
        public string key_localized { get; set; }
    }

    public class VentilationStep
    {
        public int value_raw { get; set; }
        public string value_localized { get; set; }
        public string key_localized { get; set; }
    }

    public class State
    {
        public Status status { get; set; }
        public ProgramType programType { get; set; }
        public ProgramPhase programPhase { get; set; }
        public List<int> remainingTime { get; set; }
        public List<int> startTime { get; set; }
        public TargetTemperature targetTemperature { get; set; }
        public List<Temperature> temperature { get; set; }
        public bool signalInfo { get; set; }
        public bool signalFailure { get; set; }
        public bool signalDoor { get; set; }
        public RemoteEnable remoteEnable { get; set; }
        public int light { get; set; }
        public List<object> elapsedTime { get; set; }
        public DryingStep dryingStep { get; set; }
        public VentilationStep ventilationStep { get; set; }
    }
}
