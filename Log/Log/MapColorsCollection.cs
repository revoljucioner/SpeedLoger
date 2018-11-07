using System.Linq;

namespace Log
{
    public static class MapColorsCollection
    {
        // 0x66 + RGB hex
        public static int[] ColorArray = new[]
        {
            0x66FFFFFF, // черный
            0x66FF0000, // красный
            0x66FFA500, // оранжевый
            0x66FFD700, // оранжевый
            0x6600BFFF, // deepskyblue               
            0x660000FF, // синий
            0x668000FF // purple
        };

        public static SpeedColorInterval[] SpeedColorIntervalsArray = new[]
        {
            new SpeedColorInterval(0,3,ColorArray[0]),
            new SpeedColorInterval(3,10,ColorArray[1]),
            new SpeedColorInterval(10,20,ColorArray[2]),
            new SpeedColorInterval(20,30,ColorArray[3]),
            new SpeedColorInterval(30,40,ColorArray[4]),
            new SpeedColorInterval(40,100,ColorArray[5]),
            new SpeedColorInterval(100,int.MaxValue,ColorArray[6])
        };

        public static int GetColorForSpeed(double speed)
        {
            var color = SpeedColorIntervalsArray.First(i => i.LeftSpeedBorder <= speed && i.RightSpeedBorder > speed).Color;
            return color;
        }
    }
}
