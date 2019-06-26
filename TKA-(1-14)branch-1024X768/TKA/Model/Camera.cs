using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TKA.Model
{
    public class Camera
    {
        public Camera()
        {
            ZoomRect = new Thickness();
        }

        public int Channel { get; set; }
        public Thickness ZoomRect { get; set; }
    }
}
