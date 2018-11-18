using SQLite;

namespace Log.Models
{
    [Table("DecodedSnappedPointsDb")]
    public class SnappedPointWithElevationDb: SnappedPointWithElevation
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public int TrackId { get; set; }
    }
}
