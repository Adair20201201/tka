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
            WestThroat2 = new Camera();
            EastThroat2 = new Camera();
            Track = new List<TwoCamera>();
        }

        public TwoCamera TakeCameraConfigByTrackNum(int tracknum)
        {
            return Track[tracknum-1];
        }
        public Camera WestThroat { get; set; }
        public Camera EastThroat { get; set; }
        public Camera WestThroat2 { get; set; }
        public Camera EastThroat2 { get; set; }
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
