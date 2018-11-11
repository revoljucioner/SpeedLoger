using System.Threading.Tasks;
using Log.Models;
using Xamarin.Forms.Maps;

namespace Log.Locators
{
    public interface ILocator
    {
        Task<Position> GetPositionAsync();

        Task<SnappedPoint> GetSnappedPointAsync();

    }
}
