using System;
using SQLite;
using Xamarin.Forms.Maps;

namespace Log.Models
{
    [Table("DecodedSnappedPointsDb")]
    public class SnappedPointWithElevationDb
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int TrackId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }
        public double Elevation { get; set; }

        public SnappedPointWithElevation ToSnappedPointWithElevation()
        {
            var snappedPointWithElevation = new SnappedPointWithElevation {Position = new Position(Latitude,Longitude),Time = Time, Elevation = Elevation};
            return snappedPointWithElevation;
        }
    }
}
