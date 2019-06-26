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
            //TestData();
        }

        public TwoCamera TakeCameraConfigByTrackNum(int tracknum)
        {
            if (tracknum>= 7)
            {
                tracknum = tracknum-4;
            }
            return Track[tracknum-1];
        }

        //public void TestData()
        //{
        //    WestThroat.Channel = 1;
        //    EastThroat.Channel = 2;

        //    for (int i = 3; i <= 16; i = i + 2)
        //    {
        //        Track.Add(new TwoCamera()
        //        {
        //            West = new Camera()
        //            {
        //                Channel = i,
        //                ZoomRect = new Thickness(10, 10, 100, 100)
        //            },
        //            East = new Camera()
        //            {
        //                Channel = i + 1,
        //                ZoomRect = new Thickness(10, 10, 100, 100)
        //            }
        //        });
        //    }

        //    XMLHelper.Write("d:/TrackCameraConfig.xml", this, typeof(TrackVideo));
        //}

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
