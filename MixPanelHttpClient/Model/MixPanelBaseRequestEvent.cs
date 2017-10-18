using System;
using System.Collections.Generic;
using System.Text;

namespace MixPanelHttpClient.Model
{
    public class MixPanelBaseRequestEvent<T>
    {
        public MixPanelBaseRequestEvent(string EventName, T Properties)
        {
            @event = EventName;
            properties = Properties;
        }
        public string @event { get; set; }
        public T properties { get; set; }
    }
}
