using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace Log.Locators
{
    class LocatorPluginGeolocator : ILocator
    {
        private IGeolocator geolocator;
        private TimeSpan _timeout;

        public LocatorPluginGeolocator(int desiredAccuracy, TimeSpan timeout)
        {
            geolocator = CrossGeolocator.Current;
            geolocator.DesiredAccuracy = desiredAccuracy;
            _timeout = timeout;
        }
        public async Task<Xamarin.Forms.Maps.Position> GetPositionAsync()
        {
            Plugin.Geolocator.Abstractions.Position positionGeolocator = await geolocator.GetPositionAsync(timeout: _timeout);
            var positionXamarinMaps  = new Xamarin.Forms.Maps.Position(positionGeolocator.Latitude, positionGeolocator.Longitude);
            return positionXamarinMaps;
        }
    }
}
