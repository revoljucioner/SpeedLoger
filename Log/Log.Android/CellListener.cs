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

        public string GetSimSerialNumber()
        {
            //ConnectivityManager cm = (ConnectivityManager)Forms.Context.GetSystemService(Context.ConnectivityService);
            //TelecomManager cm2 = (TelecomManager)Forms.Context.GetSystemService(Context.TelecomService);

            var simSerialNumber = _telephonyManager.SimSerialNumber;
            return simSerialNumber;
        }
    }
}
