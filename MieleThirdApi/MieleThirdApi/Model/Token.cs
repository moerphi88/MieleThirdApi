using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MieleThirdApi.Model
{
    public class Token
    {
        /*{
    "access_token": "DE_4ffe8e9614659ee918d54cc99cb356cb",
    "refresh_token": "DE_1e1f3e0b84e7b01d3e03586031d83992",
    "token_type": "Bearer",
    "expires_in": 2592000
}*/

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}
