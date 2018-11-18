namespace Log.Services.Models
{
    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Location(double _latitude, double _longitude)
        {
            latitude = _latitude;
            longitude = _longitude;
        }
    }
}
