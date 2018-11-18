using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using Log.Models;
using Log.DependenciesOS;

namespace Log.DB
{
    public class DecodedSnappedPointsDb
    {
        SQLiteConnection database;
        public DecodedSnappedPointsDb(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<SnappedPointWithElevationDb>();
        }

        public IEnumerable<SnappedPointWithElevationDb> GetItems()
        {
            return (from i in database.Table<SnappedPointWithElevationDb>() select i).ToList();

        }

        public SnappedPointWithElevationDb GetItem(int id)
        {
            SnappedPointWithElevationDb item;
            try
            {
                item = database.Get<SnappedPointWithElevationDb>(id);
            }
            catch (Exception e)
            {
                item = null;
            }
            return item;
        }

        public SnappedPointWithElevationDb[] GetItemsByTrackId(int trackId)
        {
            SnappedPointWithElevationDb[] items;
            try
            {
                items = GetItems().Where(i=>i.TrackId == trackId).ToArray();
            }
            catch (Exception e)
            {
                items = new SnappedPointWithElevationDb[]{};
            }
            return items;
        }

        public int DeleteItem(int id)
        {
            return database.Delete<SnappedPointWithElevationDb>(id);
        }

        public void DeleteItemsByTrackId(int trackId)
        {
            //var snappedPointsToDelete = GetItems().Where(i => i.TrackId == trackId);
            //var snappedPointsToDeleteIds = snappedPointsToDelete.Select(i => i.Id);
            //foreach (var snappedPointId in snappedPointsToDeleteIds)
            //{
            //    database.Delete<SnappedPointDb>(snappedPointId);
            //}
            var snappedPointsToDelete = GetItems().Where(i => i.TrackId == trackId);
            foreach (var snappedPoint in snappedPointsToDelete)
            {
                database.Delete<SnappedPointWithElevationDb>(snappedPoint.Id);
            }
        }

        public int SaveItem(SnappedPointWithElevationDb item)
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

        public List<int> SaveItems(IEnumerable<SnappedPointWithElevationDb> items)
        {
            var idList = new List<int>();
            int id;
            foreach (var item in items)
            {
                id = SaveItem(item);
                idList.Add(id);
            }

            return idList;
        }
    }
}
