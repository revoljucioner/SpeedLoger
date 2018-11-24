using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace Log.Locators
{
    public class LocatorPluginGeolocator : ILocator
    {
        private IGeolocator geolocator;
        private TimeSpan _minimumTime;
        private double _minimumDistance;


        public LocatorPluginGeolocator(TimeSpan minimumTime, double minimumDistance)
        {
            _minimumTime = minimumTime;
            _minimumDistance = minimumDistance;
        }

        public async Task StartListening(EventHandler<PositionEventArgs> eventMethod)
        {
            if (CrossGeolocator.Current.IsListening)
                return;
            await CrossGeolocator.Current.StartListeningAsync(_minimumTime, _minimumDistance, false);

            CrossGeolocator.Current.PositionChanged += eventMethod;
        }

        public async Task StopListening(EventHandler<PositionEventArgs> eventMethod)
        {
            if (!CrossGeolocator.Current.IsListening)
                return;
            await CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= eventMethod;
        }
    }

}
