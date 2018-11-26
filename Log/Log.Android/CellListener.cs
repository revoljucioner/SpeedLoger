using System.Linq;
using Android.Content;
using Android.Telephony;
using Log.DependenciesOS;
using Log.Droid;
using Log.Models;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CellListener))]
namespace Log.Droid
{
    public class CellListener : ICellAnalyzer
    {
        private readonly TelephonyManager _telephonyManager;

        public CellListener()
        {
            _telephonyManager = (TelephonyManager)Forms.Context.GetSystemService(Context.TelephonyService);
        }

        public CellData GetCellData()
        {
            var currentCellInfo = _telephonyManager.AllCellInfo.First();
            var cellDataEntity = new CellData();

            if (currentCellInfo is CellInfoWcdma)
            {
                var a = (CellInfoWcdma) currentCellInfo;
                cellDataEntity.Cid = a.CellIdentity.Cid;
                cellDataEntity.CellSignalStrength = a.CellSignalStrength.Dbm;
            }
            else if (currentCellInfo is CellInfoGsm)
            {
                var a = (CellInfoGsm) currentCellInfo;
                cellDataEntity.Cid = a.CellIdentity.Cid;
                cellDataEntity.CellSignalStrength = a.CellSignalStrength.Dbm;
            }

            return cellDataEntity;
        }
    }
}
