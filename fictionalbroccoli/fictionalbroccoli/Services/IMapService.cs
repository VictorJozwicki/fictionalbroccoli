using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms.Maps;

namespace fictionalbroccoli.Services
{
    public interface IMapService
    {
        Task<Xamarin.Forms.Maps.Position> GetCurrentLocation();
        bool IsLocationAvailable();
        void CreateMap();
        Map GetMap();
        void clearPins();
        void AddPin(double latitude, double longitude, string text, EventHandler evnt);
    }
}
