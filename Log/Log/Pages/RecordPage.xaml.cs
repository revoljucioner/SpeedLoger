using System;
using System.Globalization;
using Log.DependenciesOS;
using Log.Locators;
using Log.Models;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace Log.Pages
{
    public partial class RecordPage : ContentPage
    {
        private readonly DateTime _startTimeConst;
        private Track _track;
        private int _snappedPointsCount = 0;
        // meters
        ILocator locator;

        public RecordPage()
        {
            _startTimeConst = DateTime.UtcNow;

            InitializeComponent();

            startTime.Text = _startTimeConst.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);

            locator = new LocatorPluginGeolocator(minimumTime: TimeSpan.FromMilliseconds(0.5), minimumDistance: 1);
            locator.StartListening(CrossGeolocator_Current_PositionChanged);
            SaveTrackToDb();
        }

        private void SaveTrackToDb()
        {
            IDevice device = DependencyService.Get<IDevice>();
            _track = new Track
            { StartDateTime = _startTimeConst, DeviceId = device.GetDeviceId(), Imei = device.GetImei() };
            _track.Id = App.Database.SaveItem(_track);
        }

        public void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {
            // TODO:
            // попробовать вынести с главного потока
            Device.BeginInvokeOnMainThread(() =>
            {
                var positionGeolocator = e.Position;
                var snappedPointDb =
                    new SnappedPointDb { TrackId = _track.Id, Latitude = positionGeolocator.Latitude, Longitude = positionGeolocator.Longitude, Time = positionGeolocator.Timestamp.UtcDateTime };
                App.SnappedPointDatabase.SaveItem(snappedPointDb);
                _snappedPointsCount += 1;
            });
        }

        #region activities

        private async void ButtonStop_Clicked(object sender, EventArgs e)
        {
            if (_snappedPointsCount > 1)
            {
                await DisplayAlert("Successful", "Track is saved.", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Recorded track is too short. Saving is cancelled.", "OK");
            }
            await Navigation.PopAsync();
        }

        #endregion
    }
}
