using System;
using System.ComponentModel;
using Log.Extensions;
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
        public int Id => Track.Id;

        public string StartDate => Track.StartDateTime.ToShortDateString();
        public string StartTime => Track.StartDateTime.ToLongTimeString();

        public string Duration
        {
            get
            {
                var timeSpan = Track.EndDateTime.Subtract(Track.StartDateTime);
                var str = timeSpan.ToReadableString();
                return str;
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}