using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Log.Models;
using Log.Services.Controllers;
using Log.Services.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Log.Pages
{
    public partial class RecordsListPage : ContentPage
    {
        private List<TrackListItem> trackList;
        private SpeedServerService _speedServerService = new SpeedServerService();

        public RecordsListPage()
        {
            InitializeComponent();
            trackList = App.Database.GetItems().Where(i=>i.StatusActive).Select(i => i.ToTrackListItem()).ToList();
            recordsList.ItemsSource = trackList;
        }

        private async Task<SpeedModel> DecodeTrack(int trackId)
        {
            var snappedPoints = App.SnappedPointDatabase.GetItemsByTrackId(trackId);
            var snappedPointRequestArray = snappedPoints.Select(i=>i.ToSnappedPointRequest());
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
            var trackId = GetIdFromSenderButton(sender);

            OpenMap(trackId);
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
        }

        private async void OnOpenDecodedClicked(object sender, EventArgs e)
        {
            var trackId = GetIdFromSenderButton(sender);
            var speedModel = await DecodeTrack(trackId);
            //var snappedPointsWithElevationDb = speedModel.snappedPoints.Select(i=>(SnappedPointWithElevationDb)(i.ToSnappedPointWithElevation()));
            //
            var snappedPointsWithElevation = speedModel.snappedPoints.Select(i => (i.ToSnappedPointWithElevation()));
            var snappedPointsWithElevationDb = snappedPointsWithElevation.Select(i => i.ToSnappedPointsWithElevationDb(trackId));
            //var tt = snappedPointsWithElevationDb2.Cast<SnappedPointWithElevationDb>();
            //var tt2 = snappedPointsWithElevationDb2.Select(i=> i as SnappedPointWithElevationDb);
            //var tt3 = snappedPointsWithElevationDb2.OfType<SnappedPointWithElevationDb>();
            //
            //var b = AutoMapper.Mapper.Map<SnappedPointWithElevationDb>(speedModel.snappedPoints[0]);
            //var snappedPointsWithElevationDb = new SnappedPointWithElevationDb[0];
            //
            //snappedPointsWithElevationDb.ForEach(i=>i.TrackId=trackId);
            App.DecodedSnappedPointsDatabase.SaveItems(snappedPointsWithElevationDb);

            ////
            /// snappedPointsWithElevationDb
            /// 
            if (snappedPointsWithElevationDb.Count() < 2)
                throw new NotImplementedException();
            //var snappedPointsList = snappedPointsWithElevationDb.Select(i => i.ToSnappedPoint()).ToList();
            var snappedPointsList = snappedPointsWithElevationDb.Select(i=>i.ToSnappedPointWithElevation()).ToList();

            await Navigation.PushAsync(new MapPage(snappedPointsList), true);
        }

        #endregion
    }
}
