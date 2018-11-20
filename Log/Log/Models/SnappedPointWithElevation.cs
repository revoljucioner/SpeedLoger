namespace Log.Models
{
    public class SnappedPointWithElevation: SnappedPoint
    {
        public double Elevation { get; set; }

        public SnappedPointWithElevationDb ToSnappedPointsWithElevationDb(int trackId)
        {
            var snappedDb = new SnappedPointWithElevationDb{Position = Position, Elevation = Elevation, TrackId = trackId, Time = Time};
            return snappedDb;
        }
    }
}
