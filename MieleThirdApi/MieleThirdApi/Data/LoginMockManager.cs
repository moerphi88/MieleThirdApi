using MieleThirdApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MieleThirdApi.Data
{
    public class LoginMockManager : LoginManagerBase
    {        
        public LoginMockManager() : base() {}
        
        override public async Task<bool> LoginAsync(Credential credential)
        {
            var returnValue = false;
            if(!String.IsNullOrEmpty(credential?.User) && !String.IsNullOrEmpty(credential?.Password))
            {
                if (credential.User.Equals("math21@miele.de") && credential.Password.Equals("miele.math21"))
                {
                    returnValue = true;
                }
            }

            var tokenResponse = "{\"access_token\":\"DE_4ffe8e9614659ee918d54cc99cb356cb\",\"refresh_token\":\"DE_1e1f3e0b84e7b01d3e03586031d83992\",\"token_type\":\"Bearer\",\"expires_in\":2592000}";

            try
            {
                _token = await Task.Run(() => JsonConvert.DeserializeObject<Token>(tokenResponse));
            } catch(Exception ex)
            {
                Debug.WriteLine($"Exception in {nameof(LoginAsync)} : {ex.Message}");
                returnValue = false;
            }
            if(!(returnValue = await SaveAccessTokenToSecureStorage()))
            {
                _token = null;
            }

            return returnValue; 
        }

        override public async Task<bool> Refresh()
        {
            await Task.Delay(1000);
            _token = new Token();
            return true;
        }
    }
}
