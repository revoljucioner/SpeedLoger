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

        //public string[] records { get; set; }

        //public RecordsListPage()
        //{
        //    InitializeComponent();
        //        var tracks = App.Database.GetItems().Select(i => i.ToTrackListItem()).ToArray();
        //        recordsList.ItemsSource = tracks;
        //}

        #region activities

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return; // has been set to null, do not 'process' tapped event
            //DisplayAlert("Tapped", ((TrackListItem)e.SelectedItem).Id + " row was tapped", "OK");
            ((ListView)sender).SelectedItem = null; // de-select the row
            //DisplayAlert("Tapped", ((TrackListItem)e.SelectedItem).Id + " row was tapped", "OK");
            var trackId = ((TrackListItem)e.SelectedItem).Id;
            //var track = App.Database.GetItem(trackId);
            if (!App.Database.GetItem(trackId).Decoded)
                DecodeTrack(trackId);
            OpenMap(trackId);
        }

        //async void OnOpenClicked(object sender, EventArgs e)
        //{
        //    var b = (Button)sender;
        //    var yyy = (Track)((Button) sender).CommandParameter;
        //    await DisplayAlert("", $"{yyy.SnappedPointsArraySerialize.ToString()}", "OK");
        //}

        private void DecodeTrack(int trackId)
        {
        }

        private async void OpenMap(int trackId)
        {
            await Navigation.PushAsync(new MapPage(App.Database.GetItem(trackId)), true);
        }

        #endregion
    }
}
