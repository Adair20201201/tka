using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using TKA.Helper;

namespace TKA.Model
{
    public class TrackVideo
    {
        public TrackVideo()
        {
            WestThroat = new Camera();
            EastThroat = new Camera();
            Track = new List<TwoCamera>();
        }

        public TwoCamera TakeCameraConfigByTrackNum(int tracknum)
        {
            if (tracknum <= 5)
            {
                tracknum = tracknum - 1;
            }
            else
            {
                tracknum = tracknum -3;
            }
            return Track[tracknum-3];
        }
        public Camera WestThroat { get; set; }
        public Camera EastThroat { get; set; }

        public List<TwoCamera> Track { get; set; }
    }

    public class TwoCamera
    {
        public TwoCamera()
        {
            West = new Camera();
            East = new Camera();
        }

        public Camera West { get; set; }
        public Camera East { get; set; }
    }
}
