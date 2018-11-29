using Log.Models;

namespace Log.DependenciesOS
{
    public interface IPermissionsStorage
    {
        string[] PermissionsLocation { get;}
        string[] PermissionsPhone { get;}
        string[] PermissionsInternet { get;}
    }
}
