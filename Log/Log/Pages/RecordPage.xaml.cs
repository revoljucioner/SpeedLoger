using System;
using System.Collections.Generic;
using System.Globalization;
using Log.DependenciesOS;
using Log.Locators;
using Log.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Log.Pages
{
    public partial class RecordPage : ContentPage
    {
        private readonly DateTime startTimeConst;
        private Track track;
        private List<SnappedPoint> snappedPointRequestList;
        ILocator locator;

        public RecordPage()
        {
            startTimeConst = DateTime.UtcNow;

            InitializeComponent();

            startTime.Text = startTimeConst.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);

            Device.StartTimer(TimeSpan.FromMilliseconds(0.5), () =>
            {
                OnTimerTick();

                return true; // True = Repeat again, False = Stop the timer
            });

            IDevice device = DependencyService.Get<IDevice>();
            track = new Track { StartDateTime = startTimeConst, DeviceId = device.GetDeviceId(), Imei = device.GetImei() };
            snappedPointRequestList = new List<SnappedPoint>();
            //locator = new LocatorPluginGeolocator(desiredAccuracy: 1, timeout: TimeSpan.FromMilliseconds(100));
        }

        private async void OnTimerTick()
        {
            locator = new LocatorPluginGeolocator(desiredAccuracy: 1, timeout: TimeSpan.FromMilliseconds(100));
            var position = await locator.GetPositionAsync();
            FillTrackModel(position);
            FillFormFields(position);
        }

        private void SaveTrack()
        {
            if (snappedPointRequestList.Count > 1)
            {
                string json = JsonConvert.SerializeObject(snappedPointRequestList, Formatting.Indented);

                track.SnappedPointsArraySerialize = json;
                track.EndDateTime = DateTime.UtcNow;
                App.Database.SaveItem(track);
            }
        }

        private void FillTrackModel(Position position)
        {
            var location = new Xamarin.Forms.Maps.Position(position.Longitude, position.Latitude);
            var snappedPoint = new SnappedPoint { Position = location, Time = DateTime.UtcNow };
            snappedPointRequestList.Add(snappedPoint);
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
            SaveTrack();
        }

        #endregion
    }
}
