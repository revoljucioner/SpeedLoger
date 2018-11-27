using System.Linq;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Net;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Log.DependenciesOS;
using Log.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(PermissionsResolver))]
namespace Log.Droid
{
    public class PermissionsResolver : IPermissionsResolver
    {
        #region PermissionsGroups

        private readonly string[] _permissionsLocation =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation

        };

        private readonly string[] _permissionsPhone =
        {
            Manifest.Permission.ReadPhoneState
        };

        private readonly string[] _permissionsInternet =
        {
            Manifest.Permission.Internet
        };

        #endregion

        public void RequestLocationPermissions()
        {
            SetPermissions(_permissionsLocation, Permission.Granted);
        }

        public void RequestPhonePermissions()
        {
            SetPermissions(_permissionsPhone, Permission.Granted);
        }

        public void RequestInternetPermissions()
        {
            SetPermissions(_permissionsInternet, Permission.Granted);
        }

        #region Helpers

        private void SetPermissions(string[] permissions, Permission permissionStatus)
        {
            var permissionsToChangeStatus = permissions.Where(i => IsPermissionCheck(i) != permissionStatus).ToArray();
            if (permissionsToChangeStatus.Length == 0)
                return;
            ActivityCompat.RequestPermissions((Activity)Forms.Context, permissionsToChangeStatus, requestCode: 0);
        }

        private Permission IsPermissionCheck(string permission)
        {
            return ContextCompat.CheckSelfPermission(Forms.Context, permission);
        }

        #endregion
    }
}
