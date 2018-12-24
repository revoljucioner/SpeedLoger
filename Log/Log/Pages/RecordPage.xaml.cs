using System;
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
        private Track _track = new Track();
        private int _snappedPointsCount = 0;
        // meters
        readonly ILocator _locator;
        private Position _previousPosition = new Position(0, 0);
        public string DurationString;

        public RecordPage()
        {
            InitializeComponent();

            _locator = new LocatorPluginGeolocator(minimumTime: TimeSpan.FromMilliseconds(0.5), minimumDistance: 5);
            _locator.StartListening(CrossGeolocator_Current_PositionChanged);

            _track.StartDateTime = _startTimeConst;

            _track.Id = SaveTrackToDb(_track);
        }

        private int SaveTrackToDb(Track track)
        {
            return App.Database.SaveItem(track);
        }

        public void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {
            DurationString = DateTime.UtcNow.Subtract(_startTimeConst).ToString();
            DurationLabel.Text = DurationString.ToString();
            // TODO:
            // попробовать вынести с главного потока

            Device.BeginInvokeOnMainThread(() =>
            {
                var positionGeolocator = e.Position;

                if ((positionGeolocator.Latitude != _previousPosition.Latitude) || (positionGeolocator.Longitude != _previousPosition.Longitude))
                {
                    var snappedPointDb =
                        new SnappedPointDb
                        {
                            TrackId = _track.Id,
                            Latitude = positionGeolocator.Latitude,
                            Longitude = positionGeolocator.Longitude,
                            Time = positionGeolocator.Timestamp.UtcDateTime
                        };
                    App.SnappedPointDatabase.SaveItem(snappedPointDb);
                    _snappedPointsCount += 1;
                    _previousPosition = positionGeolocator;
                }
            });
        }

        #region activities

        private async void ButtonStop_Clicked(object sender, EventArgs e)
        {
            await _locator.StopListening(CrossGeolocator_Current_PositionChanged);

            if (_snappedPointsCount > 1)
            {
                _track.EndDateTime = DateTime.UtcNow;
                _track.StatusActive = true;
                App.Database.Update(_track);
                await DisplayAlert("Successful", "Track is saved.", "OK");
            }
            else
            {
                App.Database.DeleteItem(_track.Id);
                App.SnappedPointDatabase.DeleteItemsByTrackId(_track.Id);
                await DisplayAlert("Alert", "Recorded track is too short. Saving is cancelled.", "OK");
            }
            await Navigation.PopAsync();
        }

        #endregion
    }
}
