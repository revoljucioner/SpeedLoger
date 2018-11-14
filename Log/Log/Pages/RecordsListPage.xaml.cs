using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Log.Models;
using Xamarin.Forms;

namespace Log.Pages
{
    public partial class RecordsListPage : ContentPage
    {
        private List<TrackListItem> trackList;

        public RecordsListPage()
        {
            InitializeComponent();
            trackList = App.Database.GetItems().Select(i => i.ToTrackListItem()).ToList();
            recordsList.ItemsSource = trackList;
        }

        private void DecodeTrack(string trackId)
        {
        }

        private async void OpenMap(int trackId)
        {
            await Navigation.PushAsync(new MapPage(trackId), true);
        }

        private int GetIdFromSenderButton(object sender)
        {
            var trackListItem = (TrackListItem)((Button)sender).CommandParameter;
            return trackListItem.Id;
        }

        #region activities

        private void OnOpenClicked(object sender, EventArgs e)
        {
            var trackId = GetIdFromSenderButton(sender);

            //if (!App.Database.GetItem(trackId).Decoded)
            //    DecodeTrack(trackId);
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

        #endregion
    }
}
