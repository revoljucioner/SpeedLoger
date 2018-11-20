using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using Log.Models;
using Log.DependenciesOS;

namespace Log.DB
{
    public class TrackRepository
    {
        SQLiteConnection database;
        public TrackRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Track>();
        }

        public IEnumerable<Track> GetItems()
        {
            return (from i in database.Table<Track>() select i).ToList();

        }

        public Track GetItem(int id)
        {
            Track item;
            try
            {
                item = database.Get<Track>(id);
            }
            catch (Exception e)
            {
                item = null;
            }
            return item;
        }

        public int DeleteItem(int id)
        {
            return database.Delete<Track>(id);
        }

        public int SaveItem(Track item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                database.Insert(item);
                return item.Id;
            }
            //database.Insert(item);
            //return item.Id;
        }

        public void Update(Track track)
        {
            if (track.Id == 0)
                throw new Exception();
            database.Update(track);
        }

        public void SetDecoded(int id, bool decoded)
        {
            var track = GetItem(id);
            track.Decoded = decoded;
            database.Update(track);
        }

        //public void UpdateStatusActive(int trackId, bool statusActive)
        //{
        //    if (trackId == 0)
        //        throw new Exception();
        //    var track = GetItem(trackId);
        //    track.StatusActive = statusActive;
        //    database.Update(track);
        //}
    }
}
