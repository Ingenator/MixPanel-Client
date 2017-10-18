using MixPanelHttpClient.Enums;
using MixPanelHttpClient.Model.Constants;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MixPanelHttpClient
{
    public class Operations
    {
        internal static string DisnticntId { get; set; }
        internal void isAPIToken()
        {
            if (string.IsNullOrWhiteSpace(MixPanelClient.APIToken))
                throw new Exception("No MixPanel API Token provided");
        }
        void ValidatePremises<T>()
        {
            DataInputCleaning.validateNoPrimitiveTypes<T>();
            isAPIToken();
        }
        JObject AssignUserIdentity()
        {
            JObject joProperties = JObject.FromObject(new object());
            joProperties.Add(MPReservedNames.MPToken, MixPanelClient.APIToken);
            joProperties.Add(MPReservedNames.MPDistinctId, DisnticntId);
            return joProperties;
        }
        internal JObject DataEmptyFixer<T>(JObject jObject, T Properties)
        {
            foreach (var prop in Properties.GetType().GetRuntimeProperties())
            {
                Console.WriteLine("{0}={1}",prop.Name, prop.GetValue(Properties, null));
                var tmp = prop.GetValue(Properties, null);
                if (prop.GetValue(Properties, null) == null)
                {
                    var isOK = jObject.Remove(prop.Name);
                }
            }
            return jObject;
        }
        internal async Task<HttpResponseMessage> BaseOperation<T>(string OperationName, T Properties) where T: class
        {
            ValidatePremises<T>();
            JObject joProperties = AssignUserIdentity();            
            try
            {
                joProperties.Add(OperationName, JToken.FromObject(new Dictionary<object, object>()));
                var tmpSet = joProperties.GetValue(OperationName);
                JObject tmpSetValues = JObject.FromObject(Properties);
                tmpSet.Replace(tmpSetValues);
            }
            catch (Exception e)
            {
                throw new Exception("Operation "+ OperationName +" not defined. :" + e.Message);
            }
            return await EndPointClient.SendData(joProperties, MixPanelType.engage);
        }

        internal async Task<HttpResponseMessage> Set<T>(T Properties) where T : class
        {
            return await BaseOperation<T>(MPReservedNames.OperationSet, Properties);
        }

        internal async Task<HttpResponseMessage> SetOnce<T>(T Properties) where T : class
        {
            return await BaseOperation<T>(MPReservedNames.OperationSetOnce, Properties);
        }

        internal async Task<HttpResponseMessage> Add<T>(T Properties) where T : class
        {
            return await BaseOperation<T>(MPReservedNames.OperationAdd, Properties);
        }

        internal async Task<HttpResponseMessage> Remove<T>(T Properties) where T : class
        {
            return await BaseOperation<T>(MPReservedNames.OperationRemove, Properties);
        }
    }
}
