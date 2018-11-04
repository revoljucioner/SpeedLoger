using Newtonsoft.Json;

namespace Log.Helpers
{
    public static class DbAccessHelper
    {
        public static string TrackJson(int trackId) => JsonConvert.SerializeObject(App.Database.GetItem(trackId), Formatting.Indented);

    }
}
