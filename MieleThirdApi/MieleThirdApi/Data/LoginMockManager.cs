using MieleThirdApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    public class LoginMockManager : ILoginManager
    {
        public event EventHandler LoggedOut;
        private Token _token;

        public LoginMockManager()
        {
            
        }

        public bool IsLoggedIn()
        {
            if (_token != null)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> LoginAsync(Credential credential)
        {
            var returnValue = false;
            if(!String.IsNullOrEmpty(credential?.User) && !String.IsNullOrEmpty(credential?.Password))
            {
                if (credential.User.Equals("Hund") && credential.Password.Equals("Katze"))
                {
                    returnValue = true;
                }
            }
            var tokenResponse = "{\"access_token\":\"DE_4ffe8e9614659ee918d54cc99cb356cb\",\"refresh_token\":\"DE_1e1f3e0b84e7b01d3e03586031d83992\",\"token_type\":\"Bearer\",\"expires_in\":2592000}";
            _token = await Task.Run(() => JsonConvert.DeserializeObject<Token>(tokenResponse));
            await Task.Delay(1000);
            return returnValue; 
        }

        public async Task<bool> Logout()
        {            
            // Here goes the logout code. Delete data from device etc.
            await Task.Delay(1000);
            LoggedOut?.Invoke(this, new EventArgs());
            return true;
        }

        public async Task<bool> Refresh()
        {
            await Task.Delay(1000);
            _token = new Token();
            return true;
        }

        public string GetAccessToken()
        {
            return _token?.AccessToken;
        }
    }
}
