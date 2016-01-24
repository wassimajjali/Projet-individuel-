using System;
using System.Xml.Serialization;

namespace RadioPlayer.Models
{
    public class RadioChannel
    {
        public string ChannelName { get; set; }

        public string ChannelUri { get; set; }

        [XmlIgnore]
        public Uri ChannelStreamUri => new Uri(ChannelUri);
    }
}
