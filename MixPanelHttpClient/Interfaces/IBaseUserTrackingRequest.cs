using System;
using System.Collections.Generic;
using System.Text;

namespace MixPanelHttpClient.Interfaces
{
    public interface IBaseUserTrackingRequest
    {
        object DistinctId { get; set; }
    }
}
