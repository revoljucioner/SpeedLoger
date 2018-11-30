using Log.DB;
using Log.DependenciesOS;
using Log.Pages;
using Xamarin.Forms;

namespace Log
{
	public partial class App : Application
	{
        public const string TracksUncode = "tracksUncode.db";
	    public const string SnappedPointDatabasePath = "snappedPoints.db";
	    public const string DecodedSnappedPointsDatabasePath = "decodedSnappedPoints.db";
	    private static TrackRepository _database;
	    private static SnappedPointRepository _snappedPointDatabase;
	    private static DecodedSnappedPointsDb _decodedSnappedPointsDb;
	    private static IPermissionsResolver _permissionsResolver;
	    private static IPermissionsStorage _permissionsStorage;

        public static TrackRepository Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new TrackRepository(TracksUncode);
                }
                return _database;
            }
        }

	    public static SnappedPointRepository SnappedPointDatabase
        {
	        get
	        {
	            if (_snappedPointDatabase == null)
	            {
	                _snappedPointDatabase = new SnappedPointRepository(SnappedPointDatabasePath);
	            }
	            return _snappedPointDatabase;
	        }
	    }

	    public static DecodedSnappedPointsDb DecodedSnappedPointsDatabase
        {
	        get
	        {
	            if (_decodedSnappedPointsDb == null)
	            {
	                _decodedSnappedPointsDb = new DecodedSnappedPointsDb(DecodedSnappedPointsDatabasePath);
	            }
	            return _decodedSnappedPointsDb;
	        }
	    }

        public static IPermissionsResolver PermissionsResolver
        {
	        get
	        {
	            if (_permissionsResolver == null)
	            {
	                _permissionsResolver = DependencyService.Get<IPermissionsResolver>();
	            }
	            return _permissionsResolver;
	        }
	    }

        public static IPermissionsStorage PermissionsStorage
        {
	        get
	        {
	            if (_permissionsStorage == null)
	            {
	                _permissionsStorage = DependencyService.Get<IPermissionsStorage>();
	            }
	            return _permissionsStorage;
	        }
	    }

        public App ()
		{
            //AutoMapper.Mapper.Initialize(cfg => {
            //    cfg.CreateMap<SnappedPointWithElevation, SnappedPointWithElevationDb>();
            //    /* etc */
		    //});
		    //PermissionsResolver = DependencyService.Get<IPermissionsResolver>();
		    //PermissionsStorage = DependencyService.Get<IPermissionsStorage>();
            InitializeComponent();
            //MainPage = new NavigationPage(new MainPage());
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
