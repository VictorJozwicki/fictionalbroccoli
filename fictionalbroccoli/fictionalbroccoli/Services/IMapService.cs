using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;

namespace fictionalbroccoli.Services
{
    public interface IMapService
    {
        Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation();
        Task<string> GetCurrentAddress(Plugin.Geolocator.Abstractions.Position position);
        bool IsLocationAvailable();
        void CreateMap();
        Map GetMap();
        void clearPins();
        void AddPin(double latitude, double longitude, string text, EventHandler evnt);
    }
}
