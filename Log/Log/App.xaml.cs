using Log.DB;
using Log.Helpers;
using Log.Models;
using Log.Pages;
using Log.TestData;
using Log.Views;
using Xamarin.Forms;

namespace Log
{
	public partial class App : Application
	{
        public const string TracksUncode = "tracksUncode.db";
	    public const string SnappedPointDatabasePath = "snappedPoints.db";
	    public const string DecodedSnappedPointsDatabasePath = "decodedSnappedPoints.db";
        public static TrackRepository database;
	    public static SnappedPointRepository snappedPointDatabase;
	    public static DecodedSnappedPointsDb decodedSnappedPointsDb;

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

	    public static DecodedSnappedPointsDb DecodedSnappedPointsDatabase
        {
	        get
	        {
	            if (decodedSnappedPointsDb == null)
	            {
	                decodedSnappedPointsDb = new DecodedSnappedPointsDb(DecodedSnappedPointsDatabasePath);
	            }
	            return decodedSnappedPointsDb;
	        }
	    }

        public App ()
		{
		    //AutoMapper.Mapper.Initialize(cfg => {
		    //    cfg.CreateMap<SnappedPointWithElevation, SnappedPointWithElevationDb>();
		    //    /* etc */
		    //});

		    InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new TracksListPage());

            //MainPage = new NavigationPage(new CarouselPage2());
            //MainPage = new NavigationPage(new MainPageCarousel2());

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
