using Android.App;
using Log.DependenciesOS;
using Log.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApplication))]
namespace Log.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void TerminateApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}