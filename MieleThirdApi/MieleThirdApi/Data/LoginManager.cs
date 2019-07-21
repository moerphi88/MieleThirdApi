using MieleThirdApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    public class LoginManager : ILoginManager
    {
        public event EventHandler LoggedOut;
        private Token _token;
        //static string LoginURL = "https://api.mcs3.miele.com/thirdparty/token/?client_id=a19cbe63-3d3b-4581-a493-7f9a7f44e0ec&client_secret=vn8i8ndb5r9su2a9wcos1awz83sir4zu&vg=de-DE&grant_type=password&username=math26%40miele.de&password=miele.math26";
        static string LoginURL = "https://api.mcs3.miele.com/thirdparty/token/?client_id=a19cbe63-3d3b-4581-a493-7f9a7f44e0ec&client_secret=vn8i8ndb5r9su2a9wcos1awz83sir4zu&vg=de-DE&grant_type=password&username={0}&password={1}";

        public LoginManager()
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
            var _client = new HttpClient();

            _client.MaxResponseContentBufferSize = 256000;
            //_client.DefaultRequestHeaders.Add("Accept-Language", "de-de");

            var uri = new Uri(string.Format(LoginURL, credential.User, credential.Password));
            var returnValue = false;

            try
            {
                //create empty content to be able to send via post
                var keyValues = new List<KeyValuePair<string, string>>();
                var content = new FormUrlEncodedContent(keyValues);

                var response = await _client.PostAsync(uri,content);                

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    _token = JsonConvert.DeserializeObject<Token>(body, settings);
                    Debug.WriteLine($"Hat geklappt {_token.AccessToken}");

                    returnValue = true;
                } else
                {
                    // reset token if login fails
                    _token = null;
                }
            }
            catch (Exception ex)
            {
                // reset token if login fails
                _token = null;
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return returnValue;
        }

        public async Task<bool> Logout()
        {
            // Here goes the logout code. Delete data from device etc.
            _token = null;
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
    }
}
