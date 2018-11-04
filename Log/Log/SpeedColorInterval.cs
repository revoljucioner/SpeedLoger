using System;
using System.Collections.Generic;
using System.Text;

namespace Log
{
    public class SpeedColorInterval
    {
        public SpeedColorInterval(double leftSpeedBorder, double rightSpeedBorder, int color)
        {
            LeftSpeedBorder = leftSpeedBorder;
            RightSpeedBorder = rightSpeedBorder;
            Color = color;
        }

        public int Color;
        public double LeftSpeedBorder;
        public double RightSpeedBorder;
    }
}
