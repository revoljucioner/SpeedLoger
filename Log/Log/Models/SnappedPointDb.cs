﻿using SQLite;

namespace Log.Models
{
    [Table("SnappedPoints")]
    public class SnappedPointDb: SnappedPoint
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int TrackId { get; set; }

    }
}