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
            get
            {
                return Track.StartDateTime;
            }
            set
            {
                if (Track.StartDateTime != value)
                {
                    Track.StartDateTime = value;
                    OnPropertyChanged("StartDateTime");
                }
            }
        }

        public string Duration
        {
            get
            {
                var timeSpan = Track.EndDateTime.Subtract(Track.StartDateTime);
                var str = timeSpan.ToString("HH:mm:ss.fff");
                return str;
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}