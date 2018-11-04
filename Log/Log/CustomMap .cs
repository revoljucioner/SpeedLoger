using System.Collections.Generic;
using Log.Models;
using Xamarin.Forms.Maps;

namespace Log
{
    public class CustomMap : Map
    {
        public List<SnappedPoint> SnappedPointsList { get; set; }

        public CustomMap()
        {
            SnappedPointsList = new List<SnappedPoint>();
        }
    }
}