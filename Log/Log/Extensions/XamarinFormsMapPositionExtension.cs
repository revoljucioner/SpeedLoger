using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Text;
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
    }
}
