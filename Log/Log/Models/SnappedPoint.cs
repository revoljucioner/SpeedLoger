using System;
using Xamarin.Forms.Maps;

namespace Log.Models
{
    public class SnappedPoint
    {
        public Position Position { get; set; }
        public DateTime Time { get; set; }

        public SnappedPoint(Position position, DateTime time)
        {
            Position = position;
            Time = time;
        }
    }
}
