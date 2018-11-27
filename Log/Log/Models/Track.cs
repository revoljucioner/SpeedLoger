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
        public bool StatusActive { get; set; }
        public bool Decoded { get; set; }
    }
}