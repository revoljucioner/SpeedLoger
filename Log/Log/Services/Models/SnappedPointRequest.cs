using System;
using Log.Models;

namespace Log.Services.Models
{
    public class SnappedPointRequest
    {
        public Location Location { get; set; }
        public DateTime time { get; set; }
        public CellData CellData { get; set; }
    }
}
