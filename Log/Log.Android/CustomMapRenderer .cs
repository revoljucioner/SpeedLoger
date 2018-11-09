using System.Collections.Generic;
using System.Device.Location;
using Android.Content;
using Android.Gms.Maps.Model;
using Log.Droid;
using Log.Elements;
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
        public List<PolylineSegment> PolylineSegmentList { get; set; }
        //public double MinSpeed = double.MaxValue;
        //public double MaxSpeed = 0;

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
                PolylineSegmentList = formsMap.PolylineSegmentList;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);
            DrawPolylineSegments(PolylineSegmentList);
        }

        //private void DrawTrack()
        //{
        //    for (var i = 1; i < SnappedPointsList.Count; i++)
        //    {
        //        var snappedPointStart = SnappedPointsList[i - 1];
        //        var snappedPointEnd = SnappedPointsList[i];

        //        var speed = GetSpeedBetweenPoints(snappedPointStart, snappedPointEnd);
        //        var colorInt = MapColorsCollection.GetColorForSpeed(speed);
        //        SetLimitedSpeeds(speed);

        //        DrawPolyline(snappedPointStart, snappedPointEnd, colorInt);
        //    }
        //}

        private void DrawPolylineSegments(IEnumerable<PolylineSegment> polylineSegmentCollection)
        {
            foreach (var polylineSegment in PolylineSegmentList)
            {
                var colorInt = MapColorsCollection.GetColorForSpeed(polylineSegment.SpeedBetweenPoints());
                DrawPolylineSegment(polylineSegment, colorInt);
            }
        }

        private void DrawPolylineSegment(PolylineSegment polylineSegment, int colorInt)
        {
            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(colorInt);

            polylineOptions.Add(new LatLng(polylineSegment.SnappedPointStart.Position.Latitude, polylineSegment.SnappedPointStart.Position.Longitude));
            polylineOptions.Add(new LatLng(polylineSegment.SnappedPointEnd.Position.Latitude, polylineSegment.SnappedPointEnd.Position.Longitude));

            NativeMap.AddPolyline(polylineOptions);
        }

        //private void DrawPolyline(SnappedPoint snappedPointStart, SnappedPoint snappedPointEnd, int colorInt)
        //{
        //    var polylineOptions = new PolylineOptions();
        //    polylineOptions.InvokeColor(colorInt);

        //    polylineOptions.Add(new LatLng(snappedPointStart.Position.Latitude, snappedPointStart.Position.Longitude));
        //    polylineOptions.Add(new LatLng(snappedPointEnd.Position.Latitude, snappedPointEnd.Position.Longitude));

        //    NativeMap.AddPolyline(polylineOptions);
        //}

        //private void SetLimitedSpeeds(double speed)
        //{
        //    if (speed < MinSpeed)
        //        MinSpeed = speed;
        //    if (speed > MaxSpeed)
        //        MinSpeed = speed;
        //}

        //private double GetSpeedBetweenPoints(SnappedPoint snappedPointStart, SnappedPoint snappedPointEnd)
        //{
        //    var geoCoordinateStart = new GeoCoordinate(snappedPointStart.Position.Latitude, snappedPointStart.Position.Longitude);
        //    var geoCoordinateEnd = new GeoCoordinate(snappedPointEnd.Position.Latitude, snappedPointEnd.Position.Longitude);

        //    // km
        //    var distance = geoCoordinateStart.GetDistanceTo(geoCoordinateEnd)/1000;
        //    // h
        //    var hoursTime = (snappedPointEnd.Time - snappedPointStart.Time).TotalHours;
        //    // km/h
        //    var speed = distance / hoursTime;
        //    return speed;
        //}
    }
}