using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using Log.Models;
using Log.DependenciesOS;

namespace Log.DB
{
    public class SnappedPointRepository
    {
        SQLiteConnection database;
        public SnappedPointRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<SnappedPointDb>();
        }

        public IEnumerable<SnappedPointDb> GetItems()
        {
            return (from i in database.Table<SnappedPointDb>() select i).ToList();

        }

        public SnappedPointDb GetItem(int id)
        {
            SnappedPointDb item;
            try
            {
                item = database.Get<SnappedPointDb>(id);
            }
            catch (Exception e)
            {
                item = null;
            }
            return item;
        }

        public SnappedPointDb[] GetItemsByTrackId(int trackId)
        {
            SnappedPointDb[] items;
            try
            {
                items = GetItems().Where(i=>i.TrackId == trackId).ToArray();
            }
            catch (Exception e)
            {
                items = new SnappedPointDb[]{};
            }
            return items;
        }

        public int DeleteItem(int id)
        {
            return database.Delete<SnappedPointDb>(id);
        }

        public int SaveItem(SnappedPointDb item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
