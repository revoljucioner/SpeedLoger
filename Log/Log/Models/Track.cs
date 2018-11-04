using System;
using SQLite;

namespace Log.Models
{
    [Table("Tracks")]
    public class Track
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string DeviceId { get; set; }
        public string Imei { get; set; }
        public string SnappedPointsArraySerialize { get; set; }
        public string DecodedSnappedPointsArraySerialize { get; set; }

        public TrackListItem ToTrackListItem()
        {
            var trackListItem = new TrackListItem();
            trackListItem.Id = Id;
            trackListItem.StartDateTime = StartDateTime;
            trackListItem.EndDateTime = EndDateTime;
            trackListItem.Decoded = Decoded;

            return trackListItem;
        }

        public bool Decoded => (DecodedSnappedPointsArraySerialize != null);
    }
}