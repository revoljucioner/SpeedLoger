using System;
using System.Device.Location;
using System.Linq;
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

            SizeChanged += MoveToRegion;

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(customMap.SnappedPointsList.First().Position, Distance.FromMiles(1.0)));
        }

        private void MoveToRegion(object sender, EventArgs e)
        {
            var latitudeMin = customMap.SnappedPointsList.Select(i => i.Position.Latitude).Min();
            var latitudeMax = customMap.SnappedPointsList.Select(i => i.Position.Latitude).Max();

            var longitudeMin = customMap.SnappedPointsList.Select(i => i.Position.Longitude).Min();
            var longitudeMax = customMap.SnappedPointsList.Select(i => i.Position.Longitude).Max();

            var avgLatitude = (latitudeMin + latitudeMax) / 2;
            var avgLongitude = (longitudeMin + longitudeMax) / 2;

            var trackWidth = new GeoCoordinate(avgLatitude, longitudeMin).GetDistanceTo(new GeoCoordinate(avgLatitude, longitudeMax));
            var trackHeight = new GeoCoordinate(latitudeMin, avgLongitude).GetDistanceTo(new GeoCoordinate(latitudeMax, avgLongitude));

            var trackDimension = trackHeight / trackWidth;
            var pageDimension = Height / Width;

            var centerPosition = new Position(avgLatitude, avgLongitude);

            double diameterMeters;
            if (pageDimension >= trackDimension)
            {
                diameterMeters = trackHeight;
            }
            else
            {
                diameterMeters = trackWidth* trackDimension/ pageDimension/2;
            }

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromMeters(diameterMeters)));
        }
    }
}