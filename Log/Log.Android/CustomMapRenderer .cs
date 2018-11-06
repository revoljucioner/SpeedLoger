using System.Collections.Generic;
using System.Device.Location;
using Android.Content;
using Android.Gms.Maps.Model;
using Log;
using Log.Droid;
using Log.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Log.Droid
{
    public class CustomMapRenderer : MapRenderer
    {
        private List<SnappedPoint> SnappedPointsList { get; set; }

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                SnappedPointsList = formsMap.SnappedPointsList;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);
            DrawTrack();
        }

        private void DrawTrack()
        {
            for (var i = 1; i < SnappedPointsList.Count; i++)
            {
                DrawPolyline(SnappedPointsList[i - 1], SnappedPointsList[i]);
            }
        }

        private void DrawPolyline(SnappedPoint snappedPointStart, SnappedPoint snappedPointEnd)
        {
            var speed = GetSpeedBetweenPoints(snappedPointStart, snappedPointEnd);

            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(MapColorsCollection.GetColorForSpeed(speed));

            polylineOptions.Add(new LatLng(snappedPointStart.Position.Latitude, snappedPointStart.Position.Longitude));
            polylineOptions.Add(new LatLng(snappedPointEnd.Position.Latitude, snappedPointEnd.Position.Longitude));

            NativeMap.AddPolyline(polylineOptions);
        }

        private double GetSpeedBetweenPoints(SnappedPoint snappedPointStart, SnappedPoint snappedPointEnd)
        {
            var geoCoordinateStart = new GeoCoordinate(snappedPointStart.Position.Latitude, snappedPointStart.Position.Longitude);
            var geoCoordinateEnd = new GeoCoordinate(snappedPointEnd.Position.Latitude, snappedPointEnd.Position.Longitude);

            // km
            var distance = geoCoordinateStart.GetDistanceTo(geoCoordinateEnd)/1000;
            // h
            var hoursTime = (snappedPointEnd.Time - snappedPointStart.Time).TotalHours;
            // km/h
            var speed = distance / hoursTime;
            return speed;
        }
    }
}