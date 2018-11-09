using System.Collections.Generic;
using Log.Models;
using Xamarin.Forms.Maps;

namespace Log.Elements
{
    public class CustomMap : Map
    {
        public List<PolylineSegment> PolylineSegmentList { get; set; }
    }
}