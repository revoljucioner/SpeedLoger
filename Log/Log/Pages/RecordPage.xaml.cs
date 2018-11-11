﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Log.DependenciesOS;
using Log.Extensions;
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

        private Position previousPosition;
        private bool RecordInProgress;
        private List<SnappedPoint> snappedPointRequestList = new List<SnappedPoint>() { };
        // meters
        private double minDifferenceBetweenPoints = 10;
        ILocator locator;

        public RecordPage()
        {
            startTimeConst = DateTime.UtcNow;

            InitializeComponent();

            startTime.Text = startTimeConst.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);

            locator = new LocatorPluginGeolocator(desiredAccuracy: 1, timeout: TimeSpan.FromMilliseconds(10));
        }

        private async void SetPositionEveryTick()
        {
            var currentPosition = await locator.GetPositionAsync();
            var time = DateTime.UtcNow;

            FillTrackModel(currentPosition, time);
            FillFormFields(currentPosition);
            if (RecordInProgress)
                SetPositionEveryTick();
        }

        private async void SetPositionEveryTick2()
        {
            var currentSnappedPoint = await locator.GetSnappedPointAsync();

            FillTrackModel2(currentSnappedPoint);
            //FillFormFields(currentPosition);
            if (RecordInProgress)
                SetPositionEveryTick2();
        }

        private void SaveTrack()
        {
                string json = JsonConvert.SerializeObject(snappedPointRequestList, Formatting.Indented);

                track.SnappedPointsArraySerialize = json;
                track.EndDateTime = DateTime.UtcNow;
                App.Database.SaveItem(track);
        }

        private void FillTrackModel(Position position, DateTime time)
        {
            if (!previousPosition.IsNull())
            {
                var distance = previousPosition.ToGeoLocation().GetDistanceTo(position.ToGeoLocation());
                if (distance >= minDifferenceBetweenPoints)
                {
                    var snappedPoint = new SnappedPoint(position, time);
                    snappedPointRequestList.Add(snappedPoint);
                    previousPosition = position;
                }
            }
            else
            {
                previousPosition = position;
            }
        }

        private void FillTrackModel2(SnappedPoint snappedPoint)
        {
            var currentPosition = snappedPoint.Position;
            if (!previousPosition.IsNull())
            {
                var distance = previousPosition.ToGeoLocation().GetDistanceTo(currentPosition.ToGeoLocation());
                if (distance >= minDifferenceBetweenPoints)
                {
                    snappedPointRequestList.Add(snappedPoint);
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
            if (snappedPointRequestList.Count > 1)
            {
                SaveTrack();
            }
            else
            {
                DisplayAlert("Alert", "Recorded track is too short. Saving is cancelled.", "OK");
            }
        }

        private void ButtonStart_Clicked(object sender, EventArgs e)
        {
            if (!RecordInProgress)
            {
                IDevice device = DependencyService.Get<IDevice>();
                track = new Track
                    {StartDateTime = startTimeConst, DeviceId = device.GetDeviceId(), Imei = device.GetImei()};

                RecordInProgress = true;
                SetPositionEveryTick();
            }
        }

        private void ButtonStart2_Clicked(object sender, EventArgs e)
        {
            if (!RecordInProgress)
            {
                IDevice device = DependencyService.Get<IDevice>();
                track = new Track
                    { StartDateTime = startTimeConst, DeviceId = device.GetDeviceId(), Imei = device.GetImei() };

                RecordInProgress = true;
                SetPositionEveryTick2();
            }
        }

        #endregion
    }
}
