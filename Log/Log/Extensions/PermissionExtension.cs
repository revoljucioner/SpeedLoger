using Android.Content.PM;

namespace Log.Extensions
{
    public static class PermissionExtension
    {
        //TODO:
        // change to generic to allow work with ios permissions
        // and then rename to 'ToNativePermission'
        public static Permission ToAndroidPermission(this bool value)
        {
            return value ? Permission.Granted : Permission.Denied;
        }
    }
}
