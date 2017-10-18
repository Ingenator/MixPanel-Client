using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using MixPanelHttpClient.Enums;
using MixPanelHttpClient.Model;
using MixPanelHttpClient.Model.Constants;
using MixPanelHttpClient.Interfaces;
using System.Collections;

namespace MixPanelHttpClient
{
    public class MixPanelClient : IDisposable
    {

        public static string APIToken { get; set; }
        public static MixPanelOSType OS { get; set; }

        static People People { get; set; }

        public People GetPeople()
        {
            return People.GetInstance();
        }

        private MixPanelClient(string Token, MixPanelOSType OSType)
        {
            APIToken = Token;
            People = People.GetInstance();
            OS = OSType;
        }
        private static MixPanelClient _Instance;

        public static MixPanelClient GetInstance(string Token, MixPanelOSType OSType)
        {
            if (_Instance == null)
            {
                _Instance = new MixPanelClient(Token, OSType);
            }
            return _Instance;
        }

        public async Task<HttpResponseMessage> TrackAsync<T>(string Event, T Properties) where T : IBaseUserTrackingRequest
        {
            DataInputCleaning.validateNoPrimitiveTypes<T>(); //T cannot be Struct types
            HttpResponseMessage result = new HttpResponseMessage();
            JObject joProperties = JObject.FromObject(Properties);
            if (Properties != null)
            {
                joProperties = ReflectionPropertyAdder(joProperties, Properties);
                joProperties.Add(MPReservedNames.TrackToken, APIToken);
                joProperties.Add(MPReservedNames.TrackDistinctId, joProperties.GetValue(nameof(MPSet.DistinctId)));
                joProperties.Remove(nameof(MPSet.DistinctId));
                var MixPanelEvent = new MixPanelBaseRequestEvent<JObject>(Event, joProperties);
                result = await EndPointClient.SendData(MixPanelEvent, MixPanelType.track);
            }
            else
            {
                result = await TrackAsync(Event);
            }
            return result;
        }

        public async Task<HttpResponseMessage> TrackAsync(string Event)
        {
            JObject joProperties = new JObject();
            joProperties.Add(MPReservedNames.TrackToken, APIToken);
            var MixPanelEvent = new MixPanelBaseRequestEvent<JObject>(Event, joProperties);
            return await EndPointClient.SendData(MixPanelEvent, MixPanelType.track);
        }

        public async Task<HttpResponseMessage> TrackAsync<E, T>(E Event, T Properties) where E : class where T : class
        {
            DataInputCleaning.validateOnlyPrimitiveTypes<E>();
            DataInputCleaning.validateNoPrimitiveTypes<T>();

            JObject joProperties = JObject.FromObject(Properties);

            joProperties = ReflectionPropertyAdder(joProperties, Properties);

            joProperties.Add(MPReservedNames.TrackToken, APIToken);
            joProperties.Add(MPReservedNames.TrackDistinctId, joProperties.GetValue(nameof(MPSet.DistinctId)));
            joProperties.Remove(nameof(MPSet.DistinctId));

            var MixPanelEvent = new MixPanelBaseRequestEvent<JObject>(Event.ToString(), joProperties);
            return await EndPointClient.SendData(MixPanelEvent, MixPanelType.track);
        }

    public async Task<HttpResponseMessage> TrackAsync(string Event, object Properties)
        {
            JObject joProperties = new JObject();
            joProperties = ReflectionPropertyAdder(joProperties, Properties);
            joProperties.Add(MPReservedNames.TrackToken, APIToken);

            joProperties.Add(MPReservedNames.TrackDistinctId, joProperties.GetValue(nameof(MPSet.DistinctId)));
            joProperties.Remove(nameof(MPSet.DistinctId)); 

            var MixPanelEvent = new MixPanelBaseRequestEvent<JObject>(Event, joProperties);
            return await EndPointClient.SendData(MixPanelEvent, MixPanelType.track);
        }

        public JObject ReflectionPropertyAdder<T>(JObject joProperties, T Properties)
        {
            foreach (var property in Properties.GetType().GetRuntimeProperties())
            {
                //Console.WriteLine(property.Name);
                //Type Tipo = property.PropertyType.GetType();
                if (property.PropertyType.Name == typeof(Dictionary<,>).Name)
                {
                    var val = Properties.GetType().GetRuntimeProperty(property.Name);
                    var otra = (IDictionary)val.GetValue(Properties, null);
                    joProperties.Remove(property.Name);
                    int i = 0;
                    var lista = new List<string>();
                    foreach (var value in otra.Values)
                    {
                        if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                            lista.Add(value.ToString());
                        else
                            lista.Add(string.Empty);
                    }
                    foreach (var item in otra.Keys)
                    {
                        //Console.WriteLine("key:{0}:{1}",item.ToString(), lista[i]);    
                        if(!string.IsNullOrWhiteSpace(lista[i]))
                            joProperties.Add(item.ToString(), lista[i]);
                        i++;
                    }
                }
            }
            return joProperties;
        }

        public void Dispose()
        {
            _Instance = null;
        }
    }
}
