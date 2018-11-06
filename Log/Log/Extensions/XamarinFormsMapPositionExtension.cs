using System.Device.Location;
using Xamarin.Forms.Maps;

namespace Log.Extensions
{
    public static class XamarinFormsMapPositionExtension
    {
        public static GeoCoordinate ToGeoLocation(this Position position)
        {
            var geoCoordinate = new GeoCoordinate(position.Latitude, position.Longitude);
            return geoCoordinate;
        }

        public static bool IsNull(this Position position)
        {
            var isNull = (position.Latitude == 0) && (position.Longitude == 0);
            return isNull;
        }
    }
}
