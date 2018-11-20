using Android.Content;
using Android.Net;
using Android.Telecom;
using Android.Telephony;
using Log.DependenciesOS;
using Log.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CellListener))]
namespace Log.Droid
{
    public class CellListener :ICellAnalyzer
    {
        private TelephonyManager _telephonyManager;
        public CellListener()
        {
            _telephonyManager = (TelephonyManager)Forms.Context.GetSystemService(Context.TelephonyService);
        }

        public int GetGsmSignalStrength()
        {
            ConnectivityManager cm = (ConnectivityManager)Forms.Context.GetSystemService(Context.ConnectivityService);
            var y = cm.ActiveNetworkInfo;
            //
            TelecomManager cm2 = (TelecomManager)Forms.Context.GetSystemService(Context.TelecomService);
            //var y2 = cm2.IsInManagedCall;
            //
            var gsmSignalStrength = _telephonyManager.SignalStrength.GsmSignalStrength;
            //var ttt2 = ttt.GsmSignalStrength;
            return gsmSignalStrength;
        }
    }
}
