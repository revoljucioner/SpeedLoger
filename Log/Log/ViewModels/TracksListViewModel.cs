using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Log.ViewModels
{
    public class TracksListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TrackViewModel> Tracks { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand DeleteTrackCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        public TracksListViewModel()
        {
            var tracks = App.Database.GetItems();
            var trackViewModels = tracks.Select(i => new TrackViewModel(i));
            Tracks = new ObservableCollection<TrackViewModel>(trackViewModels);
            DeleteTrackCommand = new Command(DeleteTrack);
            BackCommand = new Command(Back);
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Back()
        {
            Navigation.PopAsync();
        }

        private void DeleteTrack(object friendObject)
        {
            TrackViewModel track = friendObject as TrackViewModel;
            if (track != null)
            {
                Tracks.Remove(track);
            }
            Back();
        }
    }
}