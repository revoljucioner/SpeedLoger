using System;
using System.Collections.Generic;
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

        #region activities

        async void OnOpenClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var trackListItem = (TrackListItem)((Button)sender).CommandParameter;
            var trackId = trackListItem.Id;

            //if (!App.Database.GetItem(trackId).Decoded)
            //    DecodeTrack(trackId);
            OpenMap(trackId);
        }

        private void DecodeTrack(string trackId)
        {
        }

        private async void OpenMap(string trackId)
        {
            await Navigation.PushAsync(new MapPage(trackId), true);
        }

        #endregion
    }
}
