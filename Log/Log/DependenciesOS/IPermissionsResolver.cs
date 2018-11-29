namespace Log.DependenciesOS
{
    public interface IPermissionsResolver
    {
        void SetPermissions(string[] permissions, bool permissionStatus);
        bool IsAllPermissionsChecked(string[] permissions);
    }
}
