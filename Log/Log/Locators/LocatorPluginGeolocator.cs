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
        }

        public void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                var positionGeolocator = e.Position;
                //var positionXamarinFormsMapsPosition =
                //    new Xamarin.Forms.Maps.Position(positionGeolocator.Latitude, positionGeolocator.Longitude);
                var snappedPointDb =
                    new SnappedPointDb { Latitude = positionGeolocator.Latitude, Longitude = positionGeolocator.Longitude, Time = positionGeolocator.Timestamp.UtcDateTime };

                //Positions.Add(position);
                //count++;
                //LabelCount.Text = $"{count} updates";
                //labelGPSTrack.Text = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                //    position.Timestamp, position.Latitude, position.Longitude,
                //    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);
                App.SnappedPointDatabase.SaveItem(snappedPointDb);

            });
        }

        public async Task StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

            //CrossGeolocator.Current.PositionChanged += PositionChanged;
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {

            //If updating the UI, ensure you invoke on main thread
            var position = e.Position;
            var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
            output += "\n" + $"Time: {position.Timestamp}";
            output += "\n" + $"Heading: {position.Heading}";
            output += "\n" + $"Speed: {position.Speed}";
            output += "\n" + $"Accuracy: {position.Accuracy}";
            output += "\n" + $"Altitude: {position.Altitude}";
            output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";
            Debug.WriteLine(output);
        }
    }
}
