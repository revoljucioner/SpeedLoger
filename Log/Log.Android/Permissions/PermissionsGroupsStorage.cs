using Android;
using Log.DependenciesOS;

namespace Log.Droid.Permissions
{
    public class PermissionsGroupsStorage: IPermissionsStorage
    {
        public string[] PermissionsLocation => new []
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation

        };

        public string[] PermissionsPhone => new[]
        {
            Manifest.Permission.ReadPhoneState
        };

        public string[] PermissionsInternet => new[]
        {
            Manifest.Permission.Internet
        };
    }
}