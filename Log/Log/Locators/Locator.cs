using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace Log.Locators
{
    public class LocatorPluginGeolocator : ILocator
    {
        private IGeolocator geolocator;
        private TimeSpan _timeout;
        private int _desiredAccuracy;


        public LocatorPluginGeolocator(int desiredAccuracy, TimeSpan timeout)
        {
            _desiredAccuracy = desiredAccuracy;
            _timeout = timeout;
        }
        public async Task<Xamarin.Forms.Maps.Position> GetPositionAsync()
        {
            geolocator = CrossGeolocator.Current;
            geolocator.DesiredAccuracy = _desiredAccuracy;

            Plugin.Geolocator.Abstractions.Position positionGeolocator = await geolocator.GetPositionAsync(timeout: _timeout);
            var positionXamarinMaps  = new Xamarin.Forms.Maps.Position(positionGeolocator.Latitude, positionGeolocator.Longitude);
            return positionXamarinMaps;
        }
    }
}
