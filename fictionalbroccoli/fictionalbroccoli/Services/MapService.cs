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

        IGeolocator locator;

        public MapService()
        {
            CreateMap();
        }

        public async void CreateMap()
        {
            var currentPosition = await GetCurrentLocation();
            var xamPositon = new Xamarin.Forms.Maps.Position(currentPosition.Latitude, currentPosition.Longitude);

            Map = new Map(MapSpan.FromCenterAndRadius(
                xamPositon,
                Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
        }

        public async Task<string> GetCurrentAddress(Plugin.Geolocator.Abstractions.Position position)
        {
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;
            var addresses = await locator.GetAddressesForPositionAsync(position);
            foreach(Address address in addresses)
            {
                if (address != null)
                    return address.Thoroughfare + " " + address.Locality + " " + address.PostalCode + " " + address.CountryName;
            }

            return "";
        }


        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            try
            {
                locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    return null;

                var currentPosition = await locator.GetLastKnownLocationAsync();

                if (currentPosition == null)
                {
                    currentPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));
                }
                    
                if (currentPosition == null)
                    return null;

                var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                        currentPosition.Timestamp, currentPosition.Latitude, currentPosition.Longitude,
                        currentPosition.Altitude, currentPosition.AltitudeAccuracy, currentPosition.Accuracy, currentPosition.Heading, currentPosition.Speed);

                Debug.WriteLine(output);

                return new Plugin.Geolocator.Abstractions.Position(currentPosition.Latitude, currentPosition.Longitude);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
                return null;
            }
        }

        public void clearPins()
        {
            Map.Pins.Clear();
        }

        public void AddPin(double latitude, double longitude, string text, EventHandler evnt)
        {
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(latitude, longitude),
                Label = "This is a pin !",
                Address = text
            };
            pin.Clicked += evnt;
            Map.Pins.Add(pin);
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public Map GetMap()
        {
            return Map;
        }
    }
}
