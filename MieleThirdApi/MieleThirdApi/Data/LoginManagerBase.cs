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
    public class LoginManagerBase : ILoginManager
    {
        protected Token _token;

        private static readonly string SECURE_STORAGE_OAUTH_KEY = "OAUTH_TOKEN";
        public LoginManagerBase(){}

        protected async Task<bool> SaveAccessTokenToSecureStorage()
        {
            try
            {
                _token.Timestamp = DateTime.Now; //add the savedAt timestamp
                var stringToBeSaved = JsonConvert.SerializeObject(_token);
                await SecureStorage.SetAsync(SECURE_STORAGE_OAUTH_KEY, stringToBeSaved);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in {nameof(SaveAccessTokenToSecureStorage)} : {ex.Message}");
                return false;
            }
        }

        protected async Task<bool> GetAccessTokenFromSecureStorage()
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

        public bool Logout()
        {
            _token = null;
            SecureStorage.Remove(SECURE_STORAGE_OAUTH_KEY);
            return true;
        }

        public string GetAccessToken()
        {
            return _token?.AccessToken;
        }

        public async Task<bool> IsLoggedIn()
        {
            if (_token == null)
            {
                // if token == null, the current session is not logged in. But Check storage if the former session has logged in
                var result = await GetAccessTokenFromSecureStorage();
                return result;
            }
            return true;
        }

        #region virtual

        public virtual Task<bool> LoginAsync(Credential credential)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Refresh()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
