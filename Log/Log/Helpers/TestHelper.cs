using System.Collections.Generic;
using System.Linq;
using Log.Models;

namespace Log.Helpers
{
    public static class TestHelper
    {
        public static List<int> TestTrackIdList = new List<int>();
        public static void CreateTestTrack(Track track)
        {
            TestTrackIdList.Add(App.Database.SaveItem(track));
        }

        public static void DropDataInDb()
        {
            TestTrackIdList = new List<int>();
            var currentTracksIds = App.Database.GetItems().Select(i => i.Id);
            foreach (var trackId in currentTracksIds)
            {
                App.Database.DeleteItem(trackId);
            }
        }
    }
}
