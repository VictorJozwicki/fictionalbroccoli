using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace fictionalbroccoli.Services
{
    public class MapService : IMapService
    {
        public Map Map
        {
            get;
            set;
        }

        public MapService()
        {
            createMap();
        }

        public async void createMap()
        {
            var currentPosition = await GetCurrentLocation();

            Map = new Map(MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.Maps.Position(currentPosition.Latitude, currentPosition.Longitude),
                Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
        }

        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));

                if (position == null)
                    return null;

                var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                        position.Timestamp, position.Latitude, position.Longitude,
                        position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

                Debug.WriteLine(output);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }

            return position;
        }

        public void addPin(Xamarin.Forms.Maps.Position position)
        {
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "This is a pin !",
                Address = "custom detail info"
            };
            Map.Pins.Add(pin);
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public Map getMap()
        {
            return Map;
        }
    }
}
