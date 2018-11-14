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
            // TODO:
            // просмотреть все эти параметры
            // выглядит так будто класс ListenerSettings можно удалить
            await CrossGeolocator.Current.StartListeningAsync(_minimumTime, _minimumDistance, true, new ListenerSettings
            {
                ActivityType = ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(1),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            });

            CrossGeolocator.Current.PositionChanged += eventMethod;
        }
    }
}
