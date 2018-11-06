using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Text;
using Xamarin.Forms.Maps;

namespace Log.Extensions
{
    public static class GeoCoordinateExtension 
    {
        public static Position ToXamarinFormsMapPosition(this GeoCoordinate geoCoordinate)
        {
            var position = new Position(geoCoordinate.Latitude, geoCoordinate.Longitude);
            return position;
        }
    }
}
