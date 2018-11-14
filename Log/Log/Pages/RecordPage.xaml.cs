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
            ((LocatorPluginGeolocator)locator).StartListening();
            locator.SetPositionChangedEvent(CrossGeolocator_Current_PositionChanged);
            StartRecording();
        }

        private void StartRecording()
        {
            IDevice device = DependencyService.Get<IDevice>();
            trackId = Guid.NewGuid().ToString();
            track = new Track
                { Id = trackId, StartDateTime = startTimeConst, DeviceId = device.GetDeviceId(), Imei = device.GetImei() };

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

        protected override void OnAppearing()
        {
            SetPosition();
        }

        private void SaveTrack()
        {
            track.EndDateTime = DateTime.UtcNow;
            App.Database.SaveItem(track);
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
                    snappedPointDb.TrackId = trackId;
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
                //var positionXamarinFormsMapsPosition =
                //    new Xamarin.Forms.Maps.Position(positionGeolocator.Latitude, positionGeolocator.Longitude);
                var snappedPointDb =
                    new SnappedPointDb{Latitude = positionGeolocator .Latitude, Longitude =  positionGeolocator.Longitude, Time = positionGeolocator.Timestamp.UtcDateTime };

                //Positions.Add(position);
                //count++;
                //LabelCount.Text = $"{count} updates";
                //labelGPSTrack.Text = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                //    position.Timestamp, position.Latitude, position.Longitude,
                //    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);
                App.SnappedPointDatabase.SaveItem(snappedPointDb);

            });
        }

        #endregion
    }
}
