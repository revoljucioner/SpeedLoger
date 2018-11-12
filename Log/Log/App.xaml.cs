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
	    public const string SnappedPointDatabasePath = "snappedPoints.db";
        public static TrackRepository database;
	    public static SnappedPointRepository snappedPointDatabase;
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

	    public static SnappedPointRepository SnappedPointDatabase
        {
	        get
	        {
	            if (snappedPointDatabase == null)
	            {
	                snappedPointDatabase = new SnappedPointRepository(SnappedPointDatabasePath);
	            }
	            return snappedPointDatabase;
	        }
	    }

        public App ()
		{
			InitializeComponent();
            //MainPage = new NavigationPage(new MainPage());
		    MainPage = new NavigationPage(new MainPageCarousel());

            // For testing:
            //TestHelper.DropDataInDb();
            //TestHelper.CreateTestTrack(TrackStorage.CommonSanFranciscoTrack);
            //TestHelper.CreateTestTrack(TrackStorage.FromLesnayaToPushkinaTrack);
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
