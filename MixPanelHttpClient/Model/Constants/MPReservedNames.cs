using System;
using System.Collections.Generic;
using System.Text;

namespace MixPanelHttpClient.Model.Constants
{
    public class MPReservedNames
    {
        internal const string TrackEvent = "event";
        internal const string TrackToken = "token";
        internal const string TrackDistinctId = "distinct_id";
        internal const string TrackTime = "time";
        internal const string TrackIp = "ip";
        internal const string TrackAlias = "alias";

        internal const string TrackProperties = "properties";
        internal const string TrackCreateAlias = "$create_alias";

        //Aka People
        internal const string MPToken = "$token";
        internal const string MPDistinctId = "$distinct_id";
        internal const string MPTime = "$time";
        internal const string MPIp = "$ip";
        internal const string MPIgnoreTime = "$ignore_time";
        internal const string MPFirstName = "$first_name";
        internal const string MPLastName = "$last_name";
        internal const string MPName = "$name";
        internal const string MPCreated = "$created";
        internal const string MPEmail = "$email";
        internal const string MPPhone = "$phone";
        internal const string MPAmount = "$amount";
        internal const string MPCity = "$city";
        internal const string MPRegion = "$region";
        internal const string MPCountry = "$country_code";
        internal const string MPTimezone = "$timezone";
        internal const string MPInitialReferrer = "$initial_referrer";
        internal const string MPInitialReferringDomain = "$initial_referring_domain";
        internal const string MPOperatingSystem = "$os";
        internal const string MPLastSeen = "$last_seen";

        //Operations
        internal const string Tmp = "temp"; //Internal use only

        internal const string OperationSet = "$set";
        internal const string OperationSetOnce = "$set_once";
        internal const string OperationAdd = "$add";
        internal const string OperationAppend = "$append";
        internal const string OperationUnion = "$union";
        internal const string OperationUnset = "$unset";
        internal const string OperationRemove = "$remove";
        internal const string OperationDelete = "$delete";
        internal const string OperationTransactions = "$transactions";

        //Movile Reserved Names

        //Android
        internal const string AndroidDeviceToken = "$android_devices";

        //iOS
        internal const string ApplePushNotificationToken = "$ios_devices";
    }
}
