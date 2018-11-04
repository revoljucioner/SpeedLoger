using Log.DB;
using Log.Helpers;
using Log.Pages;
using Log.TestData;
using Xamarin.Forms;

namespace Log
{
	public partial class App : Application
	{
        public const string TracksUncode = "tracksUncode.db";
        public static TrackRepository database;
        public static TrackRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new TrackRepository(TracksUncode);
                }
                return database;
            }
        }

        public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new MainPage());

            // For testing:
		    TestHelper.CreateTestTracks(TrackStorage.GetCommonSanFranciscoTrack);
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
