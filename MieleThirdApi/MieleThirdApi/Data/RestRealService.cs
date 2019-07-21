using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MieleThirdApi.Model;
using Newtonsoft.Json;

namespace MieleThirdApi.Data
{
    public class RestRealService : IRestApi
    {
        HttpClient client;
        Token token;

        //curl -X GET "https://api.mcs3.miele.com/v1/devices/" -H  "accept: application/json; charset=utf-8"
        static string DevicesUrl = "https://api.mcs3.miele.com/v1/devices/?language=de";

        public RestRealService()
        {
            //need to add this handler to use the API without certificate
            //var handler = new HttpClientHandler();
            //handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            //handler.ServerCertificateCustomValidationCallback =
            //    (httpRequestMessage, cert, cetChain, policyErrors) =>
            //    {
            //        return true;
            //    };
            //client = new HttpClient(handler);

            client = new HttpClient();

            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Add("Accept-Language", "de-de");
        }


        public async Task<Appliance> GetApplianceAsync(string fabNr)
        {

            throw new NotImplementedException();
        }

        public async Task<List<Appliance>> GetAppliancesListAsync()
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var uri = new Uri(string.Format(DevicesUrl, string.Empty));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "DE_4ffe8e9614659ee918d54cc99cb356cb");
            var list = new List<Appliance>();
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    var dict = JsonConvert.DeserializeObject<Dictionary<string, Appliance>>(body, settings);



                    foreach(KeyValuePair<string,Appliance> a in dict){
                        list.Add(a.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }
    }
}
