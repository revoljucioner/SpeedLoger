using System;
using Log.Models;
using Xamarin.Forms.Maps;

namespace Log.Services.Models
{
    public class SnappedPointResponse
    {
        public LocationWithElevation Location { get; set; }
        public DateTime time { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }

        //TODO:
        //move this to extensions
        public SnappedPointWithElevation ToSnappedPointWithElevation()
        {
            var position = new Position(Location.latitude, Location.longitude);
            var snappedPointWithElevation = new SnappedPointWithElevation
                {Elevation = Location.elevation, Position = position, Time = time};
            return snappedPointWithElevation;
        }
    }
}
