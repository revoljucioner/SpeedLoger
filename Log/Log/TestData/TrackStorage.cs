using System;
using Log.Models;

namespace Log.TestData
{
    public static class TrackStorage
    {
        public static Track CommonSanFranciscoTrack => new Track
        {
            StartDateTime = DateTime.Parse("2018-01-01T10:00:01.00000Z"),
            EndDateTime = DateTime.Parse("2018-01-01T10:10:12.00000Z"),
        };

        public static Track FromLesnayaToPushkinaTrack => new Track
        {
            StartDateTime = DateTime.Parse("2018-11-08T10:00:01.00000Z"),
            EndDateTime = DateTime.Parse("2018-11-08T10:10:12.00000Z"),
        };
    }
}