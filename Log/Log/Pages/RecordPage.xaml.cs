using System;
using System.Collections.Generic;
using System.Globalization;
using Log.DependenciesOS;
using Log.Extensions;
using Log.Locators;
using Log.Models;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Position = Xamarin.Forms.Maps.Position;

namespace Log.Pages
{
    public partial class RecordPage : ContentPage
    {
        private readonly DateTime startTimeConst;
        private Track track;

        private Position previousPosition;
        private bool RecordInProgress;
        private int _snappedPointsCount = 0;
        // meters
        private double minDifferenceBetweenPoints = 5;
        private string trackId;
        ILocator locator;

        public RecordPage()
        {
            startTimeConst = DateTime.UtcNow;

            InitializeComponent();

            startTime.Text = startTimeConst.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);

            locator = new LocatorPluginGeolocator(desiredAccuracy: 1, timeout: TimeSpan.FromMilliseconds(10));
            //locator.SetPositionChangedEvent(CrossGeolocator_Current_PositionChanged);
            ((LocatorPluginGeolocator)locator).StartListening(CrossGeolocator_Current_PositionChanged);
            //locator.SetPositionChangedEvent(CrossGeolocator_Current_PositionChanged);
            StartRecording();
        }

        private void StartRecording()
        {
            IDevice device = DependencyService.Get<IDevice>();
            //trackId = Guid.NewGuid().ToString();
            track = new Track
                { StartDateTime = startTimeConst, DeviceId = device.GetDeviceId(), Imei = device.GetImei() };
            track.Id = App.Database.SaveItem(track);
            RecordInProgress = true;
        }

        //private async void SetPositionEveryTick()
        //{
        //    var currentSnappedPoint = await locator.GetSnappedPointAsync();

        //    SaveSnappedPointToDb(currentSnappedPoint);
        //    _snappedPointsCount += 1;
        //    FillFormFields(currentSnappedPoint.Position);
        //    if (RecordInProgress)
        //        SetPositionEveryTick();
        //}

        private async void SetPosition()
        {
            while (RecordInProgress)
            {
                var currentSnappedPoint = await locator.GetSnappedPointAsync().ConfigureAwait(false);
                SaveSnappedPointToDb(currentSnappedPoint);
                _snappedPointsCount += 1;
                //FillFormFields(currentSnappedPoint.Position);
            }
        }

        //protected override void OnAppearing()
        //{
        //    SetPosition();
        //}

        private void SaveTrack()
        {
            //track.EndDateTime = DateTime.UtcNow;
            //App.Database.SaveItem(track);
        }

        private void SaveSnappedPointToDb(SnappedPoint snappedPoint)
        {
            var currentPosition = snappedPoint.Position;
            if (!previousPosition.IsNull())
            {
                var distance = previousPosition.ToGeoLocation().GetDistanceTo(currentPosition.ToGeoLocation());
                if (distance >= minDifferenceBetweenPoints)
                {
                    var snappedPointDb = new SnappedPointDb(snappedPoint);
                    snappedPointDb.TrackId = track.Id;
                    App.SnappedPointDatabase.SaveItem(snappedPointDb);
                    previousPosition = currentPosition;
                }
            }
            else
            {
                previousPosition = currentPosition;
            }
        }

        private void FillFormFields(Position position)
        {
            duration.Text = DateTime.UtcNow.Subtract(startTimeConst).ToString();
            longitude.Text = position.Longitude.ToString();
            latitude.Text = position.Latitude.ToString();
        }

        #region activities

        private void ButtonStop_Clicked(object sender, EventArgs e)
        {
            RecordInProgress = false;
            if (_snappedPointsCount > 1)
            {
                SaveTrack();
            }
            else
            {
                DisplayAlert("Alert", "Recorded track is too short. Saving is cancelled.", "OK");
            }
        }

        public void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                var positionGeolocator = e.Position;
                var snappedPointDb =
                    new SnappedPointDb { TrackId = 1113, Latitude = positionGeolocator.Latitude, Longitude = positionGeolocator.Longitude, Time = positionGeolocator.Timestamp.UtcDateTime };
                App.SnappedPointDatabase.SaveItem(snappedPointDb);

            });
        }

        #endregion
    }
}
