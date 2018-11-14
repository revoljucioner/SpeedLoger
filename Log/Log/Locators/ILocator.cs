using System;
using System.Threading.Tasks;
using Log.Models;
using Plugin.Geolocator.Abstractions;
using Position = Xamarin.Forms.Maps.Position;

namespace Log.Locators
{
    public interface ILocator
    {
        Task<Position> GetPositionAsync();

        Task<SnappedPoint> GetSnappedPointAsync();

        void SetPositionChangedEvent(EventHandler<PositionEventArgs> eventMethod);
    }
}
