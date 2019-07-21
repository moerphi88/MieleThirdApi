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

        public async Task<bool> LoginAsync()
        {
            var tokenResponse = "{\"access_token\":\"DE_4ffe8e9614659ee918d54cc99cb356cb\",\"refresh_token\":\"DE_1e1f3e0b84e7b01d3e03586031d83992\",\"token_type\":\"Bearer\",\"expires_in\":2592000}";
            _token = await Task.Run(() => JsonConvert.DeserializeObject<Token>(tokenResponse));
            return true;
        }

        public async Task<bool> Logout()
        {
            LoggedOut?.Invoke(this, new EventArgs());
            // Her goes the logout code. Delete data from device etc.
            await Task.Delay(1000);
            return true;
        }

        public async Task<bool> Refresh()
        {
            await Task.Delay(1000);
            _token = new Token();
            return true;
        }
    }
}
