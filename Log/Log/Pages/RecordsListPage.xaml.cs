using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Log.DependenciesOS;
using Log.Models;
using Log.Services.Controllers;
using Log.Services.Models;
using Xamarin.Forms;

namespace Log.Pages
{
    public partial class RecordsListPage : ContentPage
    {
        private List<TrackListItem> trackList;
        private SpeedServerService _speedServerService = new SpeedServerService();
        private IPermissionsResolver _permissionsResolver;

        public RecordsListPage()
        {
            _permissionsResolver = DependencyService.Get<IPermissionsResolver>();
            _permissionsResolver.RequestInternetPermissions();
            InitializeComponent();
            IsBusy = false;
            trackList = App.Database.GetItems().Where(i => i.StatusActive).Select(i => i.ToTrackListItem()).ToList();
            recordsList.ItemsSource = trackList;
        }

        private async Task<SpeedModel> DecodeTrack(int trackId)
        {
            var snappedPoints = App.SnappedPointDatabase.GetItemsByTrackId(trackId);
            var snappedPointRequestArray = snappedPoints.Select(i => i.ToSnappedPointRequest());
            var speedModelDecoded = await _speedServerService.GetSnappedPointsArrayFromSpeedServer(snappedPointRequestArray);

            return speedModelDecoded;
        }

        private async void OpenMap(int trackId)
        {
            //await Navigation.PushAsync(new MapPage(trackId), true);
        }

        private int GetIdFromSenderButton(object sender)
        {
            var trackListItem = (TrackListItem)((Button)sender).CommandParameter;
            return trackListItem.Id;
        }

        #region activities

        private async void OnOpenClicked(object sender, EventArgs e)
        {
            //_permissionsResolver.RequestInternetPermissions();
            //TurnOnLoader();
            //Task.Delay(3000);
            var trackId = GetIdFromSenderButton(sender);
            SpeedModel speedModel;
            if (!App.Database.GetItem(trackId).Decoded)
            {
                try
                {
                    speedModel = await DecodeTrack(trackId);
                }
                catch (Exception exception)
                {
                    await DisplayAlert("Error", exception.Message, "OK");
                    return;
                }
                var snappedPointsWithElevation = speedModel.snappedPoints.Select(i => (i.ToSnappedPointWithElevation()));
                var snappedPointsWithElevationDb = snappedPointsWithElevation.Select(i => i.ToSnappedPointsWithElevationDb(trackId));
                App.DecodedSnappedPointsDatabase.SaveItems(snappedPointsWithElevationDb);
                App.Database.SetDecoded(trackId, true);
            }

            var snappedPointsWithElevationFromDb = App.DecodedSnappedPointsDatabase.GetItemsByTrackId(trackId).OrderBy(i => i.Time);

            if (snappedPointsWithElevationFromDb.Count() < 2)
                throw new NotImplementedException();
            var snappedPointsList = snappedPointsWithElevationFromDb.Select(i => i.ToSnappedPointWithElevation()).ToList();

            await Navigation.PushAsync(new MapPage(snappedPointsList), true);
        }

        private void TurnOnLoader()
        {
            IsBusy = true;
            loader.IsEnabled = true;
            loader.IsVisible = true;
            loader.IsRunning = true;
        }

        private async void Afff(int trackId)
        {
            SpeedModel speedModel;
            if (!App.Database.GetItem(trackId).Decoded)
            {
                try
                {
                    speedModel = await DecodeTrack(trackId);
                }
                catch (Exception exception)
                {
                    await DisplayAlert("Error", exception.Message, "OK");
                    return;
                }
                var snappedPointsWithElevation = speedModel.snappedPoints.Select(i => (i.ToSnappedPointWithElevation()));
                var snappedPointsWithElevationDb = snappedPointsWithElevation.Select(i => i.ToSnappedPointsWithElevationDb(trackId));
                App.DecodedSnappedPointsDatabase.SaveItems(snappedPointsWithElevationDb);
                App.Database.SetDecoded(trackId, true);
            }

            var snappedPointsWithElevationFromDb = App.DecodedSnappedPointsDatabase.GetItemsByTrackId(trackId).OrderBy(i => i.Time);

            if (snappedPointsWithElevationFromDb.Count() < 2)
                throw new NotImplementedException();
            var snappedPointsList = snappedPointsWithElevationFromDb.Select(i => i.ToSnappedPointWithElevation()).ToList();

            await Navigation.PushAsync(new MapPage(snappedPointsList), true);
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var trackId = GetIdFromSenderButton(sender);
            var action = await DisplayActionSheet("Are you sure that you wanna delete Track?", "Cancel", "Delete");
            if (action == "Delete")
            {
                App.Database.DeleteItem(trackId);
                App.SnappedPointDatabase.DeleteItemsByTrackId(trackId);
            }
            //recordsList.BeginRefresh();
            trackList.Remove(trackList.Where(i => i.Id == trackId).Single());
            //recordsList.EndRefresh();
            recordsList.ItemsSource = trackList;
        }

        #endregion
    }
}
