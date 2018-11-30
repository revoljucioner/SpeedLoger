using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Log.DependenciesOS;
using Log.Droid.Permissions;
using Log.Extensions;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PermissionsResolver))]
namespace Log.Droid.Permissions
{
    public class PermissionsResolver : IPermissionsResolver
    {
        public bool IsAllPermissionsChecked(string[] permissions)
        {
            return permissions.All(i => IsPermissionCheck(i) == Android.Content.PM.Permission.Granted);
        }

        public void SetPermissions(string[] permissions, bool permissionStatus)
        {
            var nativePermissionStatus = permissionStatus.ToAndroidPermission();
            var permissionsToChangeStatus = permissions.Where(i => IsPermissionCheck(i) != nativePermissionStatus).ToArray();
            if (permissionsToChangeStatus.Length == 0)
                return;
            ActivityCompat.RequestPermissions((Activity)Forms.Context, permissionsToChangeStatus, requestCode: 0);
        }

        private Permission IsPermissionCheck(string permission)
        {
            return ContextCompat.CheckSelfPermission(Forms.Context, permission);
        }

    }
}
