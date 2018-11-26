using System;
using System.ComponentModel;
using Log.Models;

namespace Log.ViewModels
{
    public class TrackViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TracksListViewModel lvm;

        public Track Track { get; private set; }

        public TrackViewModel()
        {
            Track = new Track();
        }   

        public TrackViewModel(Track track)
        {
            Track = track;
        }

        public TracksListViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public int Id
        {
            get { return Track.Id; }
            set
            {
                if (Track.Id != value)
                {
                    Track.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public DateTime StartDateTime
        {
            get { return Track.StartDateTime; }
            set
            {
                if (Track.StartDateTime != value)
                {
                    Track.StartDateTime = value;
                    OnPropertyChanged("StartDateTime");
                }
            }
        }

        //public bool IsValid
        //{
        //    get
        //    {
        //        return ((!string.IsNullOrEmpty(Name.Trim())) ||
        //            (!string.IsNullOrEmpty(Phone.Trim())) ||
        //            (!string.IsNullOrEmpty(Email.Trim())));
        //    }
        //}

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}