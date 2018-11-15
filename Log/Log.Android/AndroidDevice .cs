using Log.DependenciesOS;
using Log.Droid;
using Xamarin.Forms;
using Android.Provider;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDevice))]
namespace Log.Droid
{
    public class AndroidDevice : IDevice
    {
        public string GetDeviceId()
        {
            var deviceId = Settings.Secure.GetString(Forms.Context.ContentResolver, Settings.Secure.AndroidId);
            return deviceId;
        }

        //public string GetImei()
        //{
        //    var telephonyManager = (Android.Telephony.TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);
        //    return telephonyManager.DeviceId;
        //}
    }
}