using System.Linq;

namespace Log
{
    public class MapColorsCollection
    {
        // 0x66 + RGB hex
        public static int[] ColorArray = new[]
        {
            0x66000000, // черный               // 0
            0x66550000, // темно-красный        // 1
            0x66FF0000, // Red                  // 2
            0x66FFA500, // Orange               // 3
            0x66ffe500, // желтый???            // 4
            //0x66FFD700, // желтый               // 5
            0x6600cd00, // салатовый            // 7
            0x66008000, // зеленый              // 6
            0x6600BFFF, // deepskyblue          // 8        
            0x660000FF, // синий                // 9
            //0x66a64dff, // light purple???      // 10 
            0x668000FF, // purple               // 11 
            0x662a0059 // dark purple?          // 12
        };

        public SpeedColorInterval[] SpeedColorIntervalsArray = new[]
        {
            new SpeedColorInterval(0,3,ColorArray[0]),
            new SpeedColorInterval(3,10,ColorArray[1]),
            new SpeedColorInterval(10,30,ColorArray[2]),
            new SpeedColorInterval(30,50,ColorArray[3]),
            new SpeedColorInterval(50,70,ColorArray[4]),
            new SpeedColorInterval(70,90,ColorArray[5]),
            new SpeedColorInterval(90,110,ColorArray[6]),
            new SpeedColorInterval(110,130,ColorArray[7]),
            new SpeedColorInterval(130,150,ColorArray[8]),
            new SpeedColorInterval(150,170,ColorArray[9]),
            new SpeedColorInterval(170,int.MaxValue,ColorArray[10]),
            //new SpeedColorInterval(190,210,ColorArray[11]),
            //new SpeedColorInterval(210,int.MaxValue,ColorArray[12])
        };

        public int GetColorForSpeed(double speed)
        {
            var color = SpeedColorIntervalsArray.First(i => i.LeftSpeedBorder <= speed && i.RightSpeedBorder > speed).SpeedColor;
            return color;
        }
    }
}
