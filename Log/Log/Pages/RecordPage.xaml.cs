using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Android.Locations;
using Log.DependenciesOS;
using Log.Locators;
using Log.Models;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace Log.Pages
{
    public partial class RecordPage : ContentPage
    {
        private readonly DateTime _startTimeConst;
        private Track _track = new Track();
        //private int _trackId;
        private int _snappedPointsCount = 0;
        // meters
        readonly ILocator _locator;
        private ICellAnalyzer _cellListener;
        private Position _previousPosition = new Position(0, 0);
        private IPermissionsResolver _permissionsResolver;
        private IPermissionsStorage _permissionsStorage;
        public string DurationLabelText;

        public RecordPage()
        {
            var _permissionsResolver2 = DependencyService.Get<IPermissionsResolver>();


            _permissionsResolver = App.PermissionsResolver;
            _permissionsStorage = App.PermissionsStorage;

            CheckPermissions();
            StartDateTime = DateTime.UtcNow;

            InitializeComponent();

            //startTime.Text = _startTimeConst.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);

            _locator = new LocatorPluginGeolocator(minimumTime: TimeSpan.FromMilliseconds(0.5), minimumDistance: 5);
            _locator.StartListening(CrossGeolocator_Current_PositionChanged);

            _cellListener = DependencyService.Get<ICellAnalyzer>();

            //_track.StartDateTime = _startTimeConst;

            _track.Id = SaveTrackToDb(_track);
        }

        private void CheckPermissions()
        {
            var aaa = _permissionsStorage.PermissionsLocation.ToList();
            aaa.AddRange(_permissionsStorage.PermissionsPhone.ToList());

            App.PermissionsResolver.SetPermissions(aaa.ToArray(), true);

            App.PermissionsResolver.SetPermissions(App.PermissionsStorage.PermissionsLocation, true);
            //
            _permissionsResolver = App.PermissionsResolver;
            _permissionsStorage = App.PermissionsStorage;
            var requiredPermissions = new List<string>();
            var pp = _permissionsResolver.IsAllPermissionsChecked(_permissionsStorage.PermissionsPhone);
            var pl = _permissionsResolver.IsAllPermissionsChecked(_permissionsStorage.PermissionsLocation);
            if (pl)
            {
                App.PermissionsResolver.SetPermissions(_permissionsStorage.PermissionsLocation, true);
                requiredPermissions.Add("Location Permission");
            }
            if (pp)
            {
                App.PermissionsResolver.SetPermissions(_permissionsStorage.PermissionsPhone, true);
                requiredPermissions.Add("Phone Permission");
            }
            if (!(pp & pl))
                DurationLabelText = $"Please allow {requiredPermissions} permissions";
        }

        private int SaveTrackToDb(Track track)
        {
            return App.Database.SaveItem(track);
        }

        public void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {
            if (!_permissionsResolver.IsAllPermissionsChecked(_permissionsStorage.PermissionsLocation))
                return;
            if (!_permissionsResolver.IsAllPermissionsChecked(_permissionsStorage.PermissionsPhone))
                return;

            DurationLabelText = DateTime.UtcNow.Subtract(StartDateTime).ToString();

            // TODO:
            // попробовать вынести с главного потока

            Device.BeginInvokeOnMainThread(() =>
            {
                var positionGeolocator = e.Position;

                if ((positionGeolocator.Latitude != _previousPosition.Latitude) || (positionGeolocator.Longitude != _previousPosition.Longitude))
                {
                    var cellData = _cellListener.GetCellData();

                    var snappedPointDb =
                        new SnappedPointDb
                        {
                            TrackId = _track.Id,
                            Latitude = positionGeolocator.Latitude,
                            Longitude = positionGeolocator.Longitude,
                            Time = positionGeolocator.Timestamp.UtcDateTime,
                            Cid = cellData.Cid,
                            CellSignalStrength = cellData.CellSignalStrength
                        };
                    App.SnappedPointDatabase.SaveItem(snappedPointDb);
                    _snappedPointsCount += 1;
                    _previousPosition = positionGeolocator;
                }
            });
        }

        #region activities

        private async void ButtonStop_Clicked(object sender, EventArgs e)
        {
            await _locator.StopListening(CrossGeolocator_Current_PositionChanged);

            if (_snappedPointsCount > 1)
            {
                _track.EndDateTime = DateTime.UtcNow;
                _track.StatusActive = true;
                App.Database.Update(_track);
                await DisplayAlert("Successful", "Track is saved.", "OK");
            }
            else
            {
                App.Database.DeleteItem(_track.Id);
                App.SnappedPointDatabase.DeleteItemsByTrackId(_track.Id);
                await DisplayAlert("Alert", "Recorded track is too short. Saving is cancelled.", "OK");
            }
            await Navigation.PopAsync();
        }

        #endregion
    }
}
