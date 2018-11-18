using System.Collections.Generic;
using System.Linq;
using Log.Models;

namespace Log.Extensions
{
    public static class SnappedPointsCollectionExtension
    {
        //public static List<PolylineSegment> ToPolylineSegmentList(this IEnumerable<SnappedPoint> snappedPointsCollection)
        //{
        //    var polylineSegmentList = new List<PolylineSegment>();

        //    var snappedPointsArray = snappedPointsCollection.ToArray();

        //    for (var i = 1; i < snappedPointsArray.Length; i++)
        //    {
        //        var snappedPointStart = snappedPointsArray[i - 1];
        //        var snappedPointEnd = snappedPointsArray[i];

        //        polylineSegmentList.Add(new PolylineSegment(snappedPointStart, snappedPointEnd));
        //    }

        //    return polylineSegmentList;
        //}

        public static List<PolylineSegment> ToPolylineSegmentList(this IEnumerable<SnappedPointWithElevation> snappedPointsCollection)
        {
            var polylineSegmentList = new List<PolylineSegment>();

            var snappedPointsArray = snappedPointsCollection.ToArray();

            for (var i = 1; i < snappedPointsArray.Length; i++)
            {
                var snappedPointStart = snappedPointsArray[i - 1];
                var snappedPointEnd = snappedPointsArray[i];

                polylineSegmentList.Add(new PolylineSegment(snappedPointStart, snappedPointEnd));
            }

            return polylineSegmentList;
        }
    }
}
