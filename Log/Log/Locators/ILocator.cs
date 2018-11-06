using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Log.Locators
{
    public interface ILocator
    {
        Task<Position> GetPositionAsync();
    }
}
