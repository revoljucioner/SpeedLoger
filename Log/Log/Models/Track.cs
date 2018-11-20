using System;
using SQLite;

namespace Log.Models
{
    [Table("Tracks")]
    public class Track
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        //[PrimaryKey, Column("_id")]
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool StatusActive { get; set; }
        public bool Decoded { get; set; }

        public TrackListItem ToTrackListItem()
        {
            var trackListItem = new TrackListItem();
            trackListItem.Id = Id;
            trackListItem.StartDateTime = StartDateTime;
            trackListItem.EndDateTime = EndDateTime;

            return trackListItem;
        }

    }
}