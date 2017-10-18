using MixPanelHttpClient.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MixPanelHttpClient.Model
{
    public class MPUserProperties
    {
        public string UUID { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Time { get; set; }
        public string Ip { get; set; }
        public string IgnoreTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Amount { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Timezone { get; set; }
        public string InitialReferrer { get; set; }
        public string InitialReferringDomain { get; set; }
        public string OperatingSystem { get; set; }
        public string LastSeen { get; set; }
    }
}
