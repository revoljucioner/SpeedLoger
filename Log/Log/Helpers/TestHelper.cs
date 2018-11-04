using System.Linq;
using Log.Models;

namespace Log.Helpers
{
    public static class TestHelper
    {
        public static int TestTrackId;
        public static void CreateTestTracks(Track track)
        {
            DropDataInDb();
            TestTrackId =  App.Database.SaveItem(track);
        }

        public static void DropDataInDb()
        {
            var currentTracksIds = App.Database.GetItems().Select(i => i.Id);
            foreach (var trackId in currentTracksIds)
            {
                App.Database.DeleteItem(trackId);
            }
        }
    }
}
