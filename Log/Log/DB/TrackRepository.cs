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

        public Track GetItem(string id)
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

        public int DeleteItem(string id)
        {
            return database.Delete<Track>(id);
        }

        public string SaveItem(Track item)
        {
            if (item.Id != "")
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                item.Id = new Guid().ToString();
                database.Insert(item);
                return item.Id;
            }
        }
    }
}
