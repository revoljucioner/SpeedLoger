using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using Log.Extensions;
using Log.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Log.Pages
{
    public partial class MapPage : ContentPage
    {
        private double borderCoeficient = 0.8;
        private List<SnappedPoint> _snappedPointsList;

        public MapPage(Track track)
        {
            InitializeComponent();

            _snappedPointsList =
                JsonConvert.DeserializeObject<SnappedPoint[]>(track.SnappedPointsArraySerialize).ToList();

            customMap.PolylineSegmentList = _snappedPointsList.ToPolylineSegmentList();

            DrawSpeedColorBoxesLayout(10, 65);
            SizeChanged += MoveToRegion;
        }

        private void DrawSpeedColorBoxesLayout(double minSpeed, double maxSpeed)
        {
            var speeds = customMap.PolylineSegmentList.Select(i=>i.SpeedBetweenPoints());
            var speedColorIntervals = MapColorsCollection.SpeedColorIntervalsArray.Where(sci => speeds.Any(speed=>speed<= sci.RightSpeedBorder&&speed>sci.LeftSpeedBorder));

            speedColorIntervals.First().LeftSpeedBorder = Math.Floor(speeds.Min());
            speedColorIntervals.Last().RightSpeedBorder = Math.Ceiling(speeds.Max());

            foreach (var speedColorInterval in speedColorIntervals)
            {
                var speedColorBox = speedColorInterval.ToSpeedColorBox();
                SpeedColorBoxesLayout.Children.Add(speedColorBox);
            }
        }

        private void MoveToRegion(object sender, EventArgs e)
        {
            var latitudeMin = _snappedPointsList.Select(i => i.Position.Latitude).Min();
            var latitudeMax = _snappedPointsList.Select(i => i.Position.Latitude).Max();

            var longitudeMin = _snappedPointsList.Select(i => i.Position.Longitude).Min();
            var longitudeMax = _snappedPointsList.Select(i => i.Position.Longitude).Max();

            var avgLatitude = (latitudeMin + latitudeMax) / 2;
            var avgLongitude = (longitudeMin + longitudeMax) / 2;

            var trackWidth = new GeoCoordinate(avgLatitude, longitudeMin).GetDistanceTo(new GeoCoordinate(avgLatitude, longitudeMax));
            var trackHeight = new GeoCoordinate(latitudeMin, avgLongitude).GetDistanceTo(new GeoCoordinate(latitudeMax, avgLongitude));

            var trackDimension = trackHeight / trackWidth;
            var pageDimension = Height / Width;

            var centerPosition = new Position(avgLatitude, avgLongitude);

            var diameterMeters =
                pageDimension >= trackDimension
                ? trackHeight
                : trackWidth * trackDimension / pageDimension / 2;

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMeters((1 / borderCoeficient) * diameterMeters)));
        }
    }
}