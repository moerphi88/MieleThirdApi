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
    public class LoginMockManager : ILoginManager
    {
        public event EventHandler LoggedOut;
        private Token _token;
        private static readonly string SECURE_STORAGE_OAUTH_KEY = "OAUTH_TOKEN";
        public LoginMockManager(){}
        private async Task<bool> SaveAccessTokenToSecureStorage()
        {
            //try
            //{
            //    _token.Timestamp = DateTime.Now; //add the savedAt timestamp
            //    var stringToBeSaved = JsonConvert.SerializeObject(_token);
            //    await SecureStorage.SetAsync(SECURE_STORAGE_OAUTH_KEY, stringToBeSaved);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"Exception in {nameof(SaveAccessTokenToSecureStorage)} : {ex.Message}");
            //    return false;
            //}
            await Task.Delay(100);
            return false;
        }

        private async Task<bool> GetAccessTokenFromSecureStorage()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync(SECURE_STORAGE_OAUTH_KEY);
                if (oauthToken != null)
                {
                    _token = JsonConvert.DeserializeObject<Token>(oauthToken);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in {nameof(GetAccessTokenFromSecureStorage)} : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsLoggedIn()
        {
            if (_token == null)
            {
                // if token == null, the current session is not logged in. But Check storage if the former seesion has logged in
                var result = await GetAccessTokenFromSecureStorage();
                return result;
            }
            return true;
        }

        public async Task<bool> LoginAsync(Credential credential)
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
            returnValue = await SaveAccessTokenToSecureStorage();

            return returnValue; 
        }

        public bool Logout()
        {
            _token = null;
            SecureStorage.Remove(SECURE_STORAGE_OAUTH_KEY);
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
