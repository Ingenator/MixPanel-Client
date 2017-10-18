using MixPanelHttpClient.Enums;
using MixPanelHttpClient.Model;
using MixPanelHttpClient.Model.Constants;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MixPanelHttpClient
{
    public class People : Operations, IDisposable
    {
        private static People _Instance;

        private People() { }

        public static People GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new People();
            }
            return _Instance;
        }

        public void Identify<T>(T disntinct_id)
        {
            DisnticntId = disntinct_id.ToString();
        }

        JObject MPReservedWordsReplacer(JObject jObject, string Property, string MPReservedName)
        {
            if (jObject.GetValue(Property) != null)
            {
                jObject.Add(MPReservedName, jObject.GetValue(Property));
                jObject.Remove(Property);
            }
            return jObject;
        }

        public async Task<HttpResponseMessage> Set(MPUserProperties Properties)
        {
            JObject tmpSetValues = JObject.FromObject(Properties);

            tmpSetValues = DataEmptyFixer(tmpSetValues, Properties);

            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Name), MPReservedNames.MPName);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Created), MPReservedNames.MPCreated);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Email), MPReservedNames.MPEmail);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Phone), MPReservedNames.MPPhone);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Time), MPReservedNames.MPTime);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Ip), MPReservedNames.MPIp);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.IgnoreTime), MPReservedNames.MPIgnoreTime);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.FirstName), MPReservedNames.MPFirstName);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.LastName), MPReservedNames.MPLastName);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Amount), MPReservedNames.MPAmount);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.City), MPReservedNames.MPCity);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Region), MPReservedNames.MPRegion);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Country), MPReservedNames.MPCountry);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.Timezone), MPReservedNames.MPTimezone);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.InitialReferrer), MPReservedNames.MPInitialReferrer);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.InitialReferringDomain), MPReservedNames.MPInitialReferringDomain);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.OperatingSystem), MPReservedNames.MPOperatingSystem);
            MPReservedWordsReplacer(tmpSetValues, nameof(MPUserProperties.LastSeen), MPReservedNames.MPLastSeen);           

            return await Set(tmpSetValues);
        }
        

        public async Task<HttpResponseMessage> setPushRegistrationId(string NotificationToken)
        {
            isAPIToken();
            JObject joProperties = new JObject();
            var tmpKP = new List<string>();
            tmpKP.Add(NotificationToken);
            switch (Enum.Parse(typeof(MixPanelOSType), Enum.GetName(typeof(MixPanelOSType),MixPanelClient.OS)))
            {
                case MixPanelOSType.Android:                
                    joProperties.Add(MPReservedNames.AndroidDeviceToken, JToken.FromObject(tmpKP));
                    joProperties.Add(MPReservedNames.MPOperatingSystem, Enum.GetName(typeof(MixPanelOSType), MixPanelOSType.Android));
                    break;
                case MixPanelOSType.iOS:
                    joProperties.Add(MPReservedNames.ApplePushNotificationToken, JToken.FromObject(tmpKP));
                    joProperties.Add(MPReservedNames.MPOperatingSystem, Enum.GetName(typeof(MixPanelOSType), MixPanelOSType.iOS));
                    break;
                default:
                    break;
            }

            return await Set(joProperties);
        }

        public void Dispose()
        {
            _Instance = null;
        }
    }
}
