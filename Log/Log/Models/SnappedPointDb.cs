using System;
using SQLite;
using Xamarin.Forms.Maps;

namespace Log.Models
{
    [Table("SnappedPointDb")]
    public class SnappedPointDb
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int TrackId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Time { get; set; }

        public SnappedPointDb()
        {
        }

        public SnappedPointDb(SnappedPoint snappedPoint)
        {
            Latitude = snappedPoint.Position.Latitude;
            Longitude = snappedPoint.Position.Longitude;
            Time = snappedPoint.Time;
        }

        public SnappedPoint ToSnappedPoint()
        {
            return new SnappedPoint(new Position(Latitude, Longitude), Time);
        }
    }
}
