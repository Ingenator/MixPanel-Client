using MixPanelHttpClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MixPanelHttpClient.Model
{
    public class BaseUserTrackRequest : IBaseUserTrackingRequest
    {
        public object DistinctId { get; set; }
    }
}
