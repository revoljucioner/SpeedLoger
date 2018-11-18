namespace Log.Services.Models
{
    public class LocationWithElevation : Location
    {
        public double elevation { get; set; }

        public LocationWithElevation(Location location, double _elevation) :base(location.latitude, location.longitude)
        {
            elevation = _elevation;
        }
    }
}
