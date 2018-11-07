using System;
using System.Device.Location;
using System.Linq;
using Android.Util;
using Log.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Log.Pages
{
    public partial class MapPage : ContentPage
    {
        public MapPage(Track track)
        {
            InitializeComponent();

            customMap.SnappedPointsList =
                JsonConvert.DeserializeObject<SnappedPoint[]>(track.SnappedPointsArraySerialize).ToList();

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(customMap.SnappedPointsList.First().Position,
                Distance.FromMiles(1.0)));
            SizeChanged += MoveToRegion;

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(customMap.SnappedPointsList.First().Position, Distance.FromMiles(1.0)));
        }

        void MoveToRegion(object sender, EventArgs e)
        {
            var pageDimension = GetPageDimension();
            var trackDimension = GetTrackDimension();


            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(customMap.SnappedPointsList.First().Position, Distance.FromMiles(1.0)));
            //customMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMeters(diameterMeters)));
        }

        private double GetPageDimension()
        {
            var screenWidth = Width;
            var screenHeight = Height;
            return Height / Width;
        }

        private double GetTrackDimension()
        {
            var latitudeMin = customMap.SnappedPointsList.Select(i => i.Position.Latitude).Min();
            var latitudeMax = customMap.SnappedPointsList.Select(i => i.Position.Latitude).Max();

            var longitudeMin = customMap.SnappedPointsList.Select(i => i.Position.Longitude).Min();
            var longitudeMax = customMap.SnappedPointsList.Select(i => i.Position.Longitude).Max();

            var avgLatitude = (latitudeMin + latitudeMax) / 2;
            var avgLongitude = (longitudeMin + longitudeMax) / 2;

            var trackWidth = new GeoCoordinate(latitudeMin , avgLongitude).GetDistanceTo(new GeoCoordinate(latitudeMax, avgLongitude));
            var trackHeight = new GeoCoordinate(avgLatitude, longitudeMin).GetDistanceTo(new GeoCoordinate(avgLatitude, longitudeMax));
            var trackDimension = trackHeight/ trackWidth;

            return trackDimension;
        }
    }
}