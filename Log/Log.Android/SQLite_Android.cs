using System;
using Log.Droid;
using System.IO;
using Xamarin.Forms;
using Log.DependenciesOS;

[assembly: Dependency(typeof(SQLite_Android))]
namespace Log.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
    }
}