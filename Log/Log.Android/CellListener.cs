using Android;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Telecom;
using Android.Telephony;
using Log.DependenciesOS;
using Log.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CellListener))]
namespace Log.Droid
{
    public class CellListener : ICellAnalyzer
    {
        private readonly TelephonyManager _telephonyManager;
        private readonly ConnectivityManager _connectivityManager;
        private readonly TelecomManager _telecomManager;

        public CellListener()
        {
            _telephonyManager = (TelephonyManager)Forms.Context.GetSystemService(Context.TelephonyService);
            _connectivityManager = (ConnectivityManager)Forms.Context.GetSystemService(Context.ConnectivityService);
            _telecomManager = (TelecomManager)Forms.Context.GetSystemService(Context.TelecomService);
        }

        public string GetSimSerialNumber()
        {
            return _telephonyManager.SimSerialNumber;
        }

        public string GetAllCellData()
        {
            return _telephonyManager.SimSerialNumber;
        }
    }
}
