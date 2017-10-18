using System;
using System.Collections.Generic;
using System.Text;

namespace MixPanelHttpClient.Model
{
    public class MPSet : BaseUserTrackRequest
    {
        public MPUserProperties set { get; set; }
    }

    public class MPSetOnce : BaseUserTrackRequest
    {
        public MPUserProperties set_once { get; set; }
    }

}
