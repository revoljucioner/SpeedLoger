namespace Log.DependenciesOS
{
    public interface IPermissionsResolver
    {
        void RequestLocationPermissions();
        void RequestPhonePermissions();
    }
}
