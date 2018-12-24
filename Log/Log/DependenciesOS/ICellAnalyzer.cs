using System;
using Log.Models;

namespace Log.DependenciesOS
{
    [Obsolete("Created for cell analyze in diploma")]
    public interface ICellAnalyzer
    {
        CellData GetCellData();
    }
}
