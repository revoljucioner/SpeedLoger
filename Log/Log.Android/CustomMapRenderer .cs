using System.Collections.Generic;
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
        public List<PolylineSegment> PolylineSegmentList { get; set; }

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
                PolylineSegmentList = formsMap.PolylineSegmentList;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);
            DrawPolylineSegments(PolylineSegmentList);
        }

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
    }
}