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
            CreateMap();
        }

        public async void CreateMap()
        {
            var currentPosition = await GetCurrentLocation();

            Map = new Map(MapSpan.FromCenterAndRadius(
                currentPosition,
                Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
        }

        public async Task<Xamarin.Forms.Maps.Position> GetCurrentLocation()
        {
            var position = new Xamarin.Forms.Maps.Position(0, 0);

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    return position;
                }

                var currentPosition = await locator.GetLastKnownLocationAsync();

                if (currentPosition != null)
                {
                    return new Xamarin.Forms.Maps.Position(currentPosition.Latitude, currentPosition.Longitude);
                }

                currentPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));

                if (currentPosition == null)
                    return position;

                var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                        currentPosition.Timestamp, currentPosition.Latitude, currentPosition.Longitude,
                        currentPosition.Altitude, currentPosition.AltitudeAccuracy, currentPosition.Accuracy, currentPosition.Heading, currentPosition.Speed);

                Debug.WriteLine(output);

                return new Xamarin.Forms.Maps.Position(currentPosition.Latitude, currentPosition.Longitude);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }

            return position;
        }

        public void AddPin(Xamarin.Forms.Maps.Position position)
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

        public Map GetMap()
        {
            return Map;
        }
    }
}
