using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MieleThirdApi.Model
{
    public class Device
    {
        [JsonProperty("ident")]
        public string Ident { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}
