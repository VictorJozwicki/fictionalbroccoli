using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;

namespace fictionalbroccoli.Services
{
    public interface IMapService
    {
        Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation();
        bool IsLocationAvailable();
        void createMap();
        Map getMap();
        void addPin(Xamarin.Forms.Maps.Position position);
    }
}
