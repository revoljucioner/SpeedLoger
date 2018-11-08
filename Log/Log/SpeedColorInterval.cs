using System;
using Log.Elements;
using Log.Extensions;
using Xamarin.Forms;

namespace Log
{
    public class SpeedColorInterval
    {
        public SpeedColorInterval(double leftSpeedBorder, double rightSpeedBorder, int color)
        {
            LeftSpeedBorder = leftSpeedBorder;
            RightSpeedBorder = rightSpeedBorder;
            SpeedColor = color;
        }

        public int SpeedColor;
        public double LeftSpeedBorder;
        public double RightSpeedBorder;

        public SpeedColorBox ToSpeedColorBox()
        {
            var color = Color.FromUint((uint)SpeedColor);

            var speedString = $"{LeftSpeedBorder} - {RightSpeedBorder}";
            var speedColorBox = new SpeedColorBox(color, speedString);
            return speedColorBox;
        }
    }
}
