using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Log.Models;
using Log.Views;
using Xamarin.Forms;

namespace Log.ViewModels
{
    public class TracksListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TrackViewModel> Tracks { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateFriendCommand { protected set; get; }
        public ICommand DeleteFriendCommand { protected set; get; }
        public ICommand DeleteTrackCommand { protected set; get; }
        //public ICommand SaveFriendCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        FriendViewModel selectedFriend;

        public INavigation Navigation { get; set; }

        public TracksListViewModel()
        {
            //
            Track track1 = new Track{Id = 111, StartDateTime = DateTime.Now};
            Track track2 = new Track{Id = 222, StartDateTime = DateTime.Now.AddDays(1)};
            //
            Tracks = new ObservableCollection<TrackViewModel>();
            Tracks.Add(new TrackViewModel(track1));
            Tracks.Add(new TrackViewModel(track2));
            CreateFriendCommand = new Command(CreateFriend);
            DeleteFriendCommand = new Command(DeleteFriend);
            DeleteTrackCommand = new Command(DeleteTrack);
            //SaveFriendCommand = new Command(SaveFriend);
            BackCommand = new Command(Back);
        }

        public FriendViewModel SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (selectedFriend != value)
                {
                    FriendViewModel tempFriend = value;
                    selectedFriend = null;
                    OnPropertyChanged("SelectedFriend");
                    Navigation.PushAsync(new FriendPage(tempFriend));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void CreateFriend()
        {
            //Navigation.PushAsync(new FriendPage(new TrackViewModel() { ListViewModel = this }));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }
        //private void SaveFriend(object friendObject)
        //{
        //    TrackViewModel friend = friendObject as TrackViewModel;
        //    if (friend != null && friend.IsValid)
        //    {
        //        Friends.Add(friend);
        //    }
        //    Back();
        //}
        private void DeleteFriend(object friendObject)
        {
            TrackViewModel friend = friendObject as TrackViewModel;
            if (friend != null)
            {
                Tracks.Remove(friend);
            }
            Back();
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