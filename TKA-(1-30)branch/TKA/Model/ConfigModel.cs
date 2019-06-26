using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TKA.Helper;

namespace TKA.Model
{
    public class MultiChannelInfo
    {
        public string ChannelStartPoint { get; set; }
        public int ChannelCount { get; set; }
    }

    public class ConfigModel
    {
        public List<TrackModel> LTM { get; set; }

        public List<string> ChannelCollection { get; set; }
        public List<MultiChannelInfo> MultiChannelCollections { get; set; }

       
       
        //public string PrimaryWatchSetting { get; set; }
        //public string MinorWatchSetting { get; set; }

        //public string SpeekAddress { get; set; }

        public ConfigModel()
        {
            ChannelCollection = new List<string>();
            LTM = new List<TrackModel>();
        }
    }
}
