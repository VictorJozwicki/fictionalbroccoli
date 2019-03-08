using System;
using System.Threading.Tasks;
using fictionalbroccoli.Models;
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
        void AddPin(Registration registration, EventHandler evnt);
        Task RequestLocationPermission();
    }
}
