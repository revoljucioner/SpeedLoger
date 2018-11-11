using System.Device.Location;

namespace Log.Models
{
    public class PolylineSegment
    {
        public SnappedPoint SnappedPointStart { get; set; }
        public SnappedPoint SnappedPointEnd { get; set; }

        public PolylineSegment(SnappedPoint snappedPointStart, SnappedPoint snappedPointEnd)
        {
            SnappedPointStart = snappedPointStart;
            SnappedPointEnd = snappedPointEnd;
        }

        public double SpeedBetweenPoints()
        {
            var geoCoordinateStart = new GeoCoordinate(SnappedPointStart.Position.Latitude, SnappedPointStart.Position.Longitude);
            var geoCoordinateEnd = new GeoCoordinate(SnappedPointEnd.Position.Latitude, SnappedPointEnd.Position.Longitude);

            // km
            var distance = geoCoordinateStart.GetDistanceTo(geoCoordinateEnd) / 1000;
            // h
            var hoursTime = (SnappedPointEnd.Time - SnappedPointStart.Time).TotalHours;
            // km/h
            var speed = distance / hoursTime;
            return speed;
        }
    }
}
