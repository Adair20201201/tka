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
            WestThroat1 = new Camera();
            WestThroat2 = new Camera();
            WestThroat3 = new Camera();
            WestThroat4 = new Camera();

            EastThroat1 = new Camera();
            EastThroat2 = new Camera();
            EastThroat3 = new Camera();
            EastThroat4 = new Camera();

            Track = new List<TwoCamera>();
        }

        public TwoCamera TakeCameraConfigByTrackNum(string tracknum)
        {
            int ix=0;
            if (tracknum.Contains("A"))
            {
                ix = Math.Abs(int.Parse(tracknum.Substring(1)) - 16);
            }
            else if (tracknum.Contains("B"))
            {
                ix = Math.Abs(int.Parse(tracknum.Substring(1))-32);
            }
            return Track[ix];
        }
        public Camera WestThroat1 { get; set; }
        public Camera WestThroat2 { get; set; }
        public Camera WestThroat3 { get; set; }
        public Camera WestThroat4 { get; set; }
        public Camera EastThroat1 { get; set; }
        public Camera EastThroat2 { get; set; }
        public Camera EastThroat3 { get; set; }
        public Camera EastThroat4 { get; set; }

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
