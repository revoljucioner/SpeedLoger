using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Log.Pages;
using Log.Services.Controllers;
using Log.Services.Models;
using Xamarin.Forms;

namespace Log.ViewModels
{
    public class TracksListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TrackViewModel> Tracks { get; set; }

        private SpeedServerService _speedServerService = new SpeedServerService();
        public event PropertyChangedEventHandler PropertyChanged;
        private Page _page; 

        public ICommand DeleteTrackCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public ICommand OpenTrackCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        public TracksListViewModel(Page page)
        {
            if (page==null)
                throw new Exception();
            var tracks = App.Database.GetItems().Where(i => i.StatusActive);
            var trackViewModels = tracks.Select(i => new TrackViewModel(i));
            Tracks = new ObservableCollection<TrackViewModel>(trackViewModels);
            DeleteTrackCommand = new Command(DeleteTrackAsync);
            BackCommand = new Command(Back);
            OpenTrackCommand = new Command(OpenTrackAsync);
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private async void DeleteTrackAsync(object friendObject)
        {
            var track = friendObject as TrackViewModel;

            var trackId = track.Id;
            var action = await _page.DisplayActionSheet("Are you sure that you wanna delete Track?", "Cancel", "Delete");
            if (action != "Delete")
                return;
            App.Database.DeleteItem(trackId);
            App.SnappedPointDatabase.DeleteItemsByTrackId(trackId);
            Tracks.Remove(track);
        }

        private async void OpenTrackAsync(object friendObject)
        {
            var track = friendObject as TrackViewModel;
            var trackId = track.Id;
            SpeedModel speedModel;
            if (!App.Database.GetItem(trackId).Decoded)
            {
                try
                {
                    speedModel = await DecodeTrack(trackId);
                }
                catch (Exception exception)
                {
                    await _page.DisplayAlert("Error", exception.Message, "OK");
                    return;
                }
                var snappedPointsWithElevation = speedModel.snappedPoints.Select(i => (i.ToSnappedPointWithElevation()));
                var snappedPointsWithElevationDb = snappedPointsWithElevation.Select(i => i.ToSnappedPointsWithElevationDb(trackId));
                App.DecodedSnappedPointsDatabase.SaveItems(snappedPointsWithElevationDb);
                App.Database.SetDecoded(trackId, true);
            }

            var snappedPointsWithElevationFromDb = App.DecodedSnappedPointsDatabase.GetItemsByTrackId(trackId).OrderBy(i => i.Time);

            if (snappedPointsWithElevationFromDb.Count() < 2)
                await _page.DisplayAlert("Error", "This track is too short, please remove it", "OK");
            var snappedPointsList = snappedPointsWithElevationFromDb.Select(i => i.ToSnappedPointWithElevation()).ToList();

            await Navigation.PushAsync(new MapPage(snappedPointsList), true);
        }

        private async Task<SpeedModel> DecodeTrack(int trackId)
        {
            var snappedPoints = App.SnappedPointDatabase.GetItemsByTrackId(trackId);
            var snappedPointRequestArray = snappedPoints.Select(i => i.ToSnappedPointRequest());
            var speedModelDecoded = await _speedServerService.GetSnappedPointsArrayFromSpeedServer(snappedPointRequestArray);

            return speedModelDecoded;
        }
    }
}