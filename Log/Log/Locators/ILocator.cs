using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace Log.Locators
{
    public interface ILocator
    {
        Task StartListening(EventHandler<PositionEventArgs> eventMethod);
        Task StopListening(EventHandler<PositionEventArgs> eventMethod);
    }
}
