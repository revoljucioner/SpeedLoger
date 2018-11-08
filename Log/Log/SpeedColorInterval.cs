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
            //var colorHex = SpeedColor.ToString().Substring(4, 9);
            //var color = Xamarin.Forms.Color.FromHex(colorHex);


            byte[] values = BitConverter.GetBytes(SpeedColor);
            if (!BitConverter.IsLittleEndian) Array.Reverse(values);

            Color color = Color.FromRgb(values[0], values[1], values[2]);

            //
            //var yyy = Color.Red.ToHexString();
            var tttt = SpeedColor.ToColor();
            //var yyy = Color.Red.ToHexString();
            //var tttt2 = Convert.ToInt32(Color.Red.ToHexString());
            //

            var speedString = $"{LeftSpeedBorder} - {RightSpeedBorder}";
            var speedColorBox = new SpeedColorBox(color, speedString);
            return speedColorBox;
        }
    }
}
