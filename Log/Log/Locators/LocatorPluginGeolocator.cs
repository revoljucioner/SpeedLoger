using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Log.Models;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

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

        public async Task<SnappedPoint> GetSnappedPointAsync()
        {
            geolocator = CrossGeolocator.Current;
            geolocator.DesiredAccuracy = _desiredAccuracy;

            Plugin.Geolocator.Abstractions.Position positionGeolocator = await geolocator.GetPositionAsync(timeout: _timeout);
            var positionXamarinMaps = new Xamarin.Forms.Maps.Position(positionGeolocator.Latitude, positionGeolocator.Longitude);
            var snappedPoint = new SnappedPoint ( positionXamarinMaps ,  positionGeolocator.Timestamp.UtcDateTime);
            return snappedPoint;
        }

        public void  SetPositionChangedEvent(EventHandler<PositionEventArgs> eventMethod)
        {
            //geolocator.PositionChanged += CrossGeolocator_Current_PositionChanged;
            //geolocator.PositionChanged += CrossGeolocator_Current_PositionChanged;
        }

        public void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                var positionGeolocator = e.Position;
                var snappedPointDb =
                    new SnappedPointDb { TrackId = 1111, Latitude = positionGeolocator.Latitude, Longitude = positionGeolocator.Longitude, Time = positionGeolocator.Timestamp.UtcDateTime };
                App.SnappedPointDatabase.SaveItem(snappedPointDb);

            });
        }

        public async Task StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(1),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            });

            CrossGeolocator.Current.PositionChanged += CrossGeolocator_Current_PositionChanged;
        }
    }
}
