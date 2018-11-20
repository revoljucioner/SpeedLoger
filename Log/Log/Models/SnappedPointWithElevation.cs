namespace Log.Models
{
    public class SnappedPointWithElevation: SnappedPoint
    {
        public double Elevation { get; set; }

        public SnappedPointWithElevationDb ToSnappedPointsWithElevationDb(int trackId)
        {
            var snappedDb = new SnappedPointWithElevationDb{Latitude = Position.Latitude, Longitude = Position.Longitude, Elevation = Elevation, TrackId = trackId, Time = Time};
            return snappedDb;
        }
    }
}
