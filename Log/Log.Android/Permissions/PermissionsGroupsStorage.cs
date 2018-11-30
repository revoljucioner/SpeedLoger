using Android;
using Log.DependenciesOS;
using Log.Droid.Permissions;

[assembly: Xamarin.Forms.Dependency(typeof(PermissionsGroupsStorage))]
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