using MixPanelHttpClient.Enums;
using MixPanelHttpClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MixPanelHttpClient
{
    public class EndPointClient
    {
        static readonly HttpClient _client = new HttpClient();
        static readonly Encoding Encoding = Encoding.UTF8;

        public static async Task<HttpResponseMessage> SendData<T>(T Data, MixPanelType MixPanelMessage)
        {
            var des = JsonConvert.SerializeObject(Data); //for debugging
            string data = Convert.ToBase64String(Encoding.GetBytes(JsonConvert.SerializeObject(Data)));
            var res = await _client.GetAsync(RequestURI(data, MixPanelMessage));
            ResultResponse(res);
            return res;
        }

        static void ResultResponse(HttpResponseMessage response)
        {
            var json = JsonConvert.DeserializeObject<MPVerboseResponse>(response.Content.ReadAsStringAsync().Result);
            if (json.status != "1")
                throw new Exception("Unable to send tracking information to Mixpanel, please verify data provided, error: " + json.error);
        }

        static Uri RequestURI<T>(T data, MixPanelType type)
        {
            return new Uri("http://api.mixpanel.com/" + type + "/?verbose=1;data=" + data);
        }
    }
}
