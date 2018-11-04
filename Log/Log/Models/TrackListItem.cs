using System;

namespace Log.Models
{
    public class TrackListItem
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool Decoded { get; set; }
    }
}


