using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace Log.Locators
{
    class Locator
    {
        private IGeolocator geolocator;
        private TimeSpan _timeout;

        public Locator(int desiredAccuracy, TimeSpan timeout)
        {
            geolocator = CrossGeolocator.Current;
            geolocator.DesiredAccuracy = desiredAccuracy;
            _timeout = timeout;
        }
        public async Task<Position> GetLocationAsync()
        {
            var position = await geolocator.GetPositionAsync(timeout: _timeout);
            return position;
        }
    }
}
