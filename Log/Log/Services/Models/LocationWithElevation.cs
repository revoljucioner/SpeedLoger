using Newtonsoft.Json;

namespace Log.Services.Models
{
    public class LocationWithElevation : Location
    {
        public double elevation { get; set; }

        [JsonConstructor]
        private LocationWithElevation()
        {
        }

        public LocationWithElevation(Location location, double _elevation) :base(location.latitude, location.longitude)
        {
            elevation = _elevation;
        }
    }
}
