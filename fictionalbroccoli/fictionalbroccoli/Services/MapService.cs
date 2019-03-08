using System;
using System.Diagnostics;
using System.Threading.Tasks;
using fictionalbroccoli.Models;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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


            Map = new Map(MapSpan.FromCenterAndRadius(
                new Xamarin.Forms.Maps.Position(0, 0),
                Distance.FromMiles(0.5)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var currentPosition = await GetCurrentLocation();
            var xamPositon = new Xamarin.Forms.Maps.Position(currentPosition.Latitude, currentPosition.Longitude);

            moveTo(xamPositon);
        }

        public void moveTo(Xamarin.Forms.Maps.Position position)
        {
            Map.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                position, Distance.FromMiles(0.5)));
        }

        public async Task<string> GetCurrentAddress(Plugin.Geolocator.Abstractions.Position position)
        {
            locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;
            var addresses = await locator.GetAddressesForPositionAsync(position);
            foreach (Address address in addresses)
            {
                if (address != null)
                    return address.Thoroughfare + " " + address.Locality + " " + address.PostalCode + " " + address.CountryName;
            }

            return "";
        }


        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocation()
        {
            var notAPosition = new Plugin.Geolocator.Abstractions.Position(-1, -1);
            try
            {
                locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    return notAPosition;

                //Plugin.Geolocator.Abstractions.Position currentPosition = null;
                var currentPosition = await locator.GetLastKnownLocationAsync();

                if (currentPosition == null)
                {
                    currentPosition = await locator.GetPositionAsync(TimeSpan.FromSeconds(20));
                }
                    
                if (currentPosition == null)
                    return notAPosition;

                var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                        currentPosition.Timestamp, currentPosition.Latitude, currentPosition.Longitude,
                        currentPosition.Altitude, currentPosition.AltitudeAccuracy, currentPosition.Accuracy, currentPosition.Heading, currentPosition.Speed);

                Debug.WriteLine(output);

                return new Plugin.Geolocator.Abstractions.Position(currentPosition.Latitude, currentPosition.Longitude);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
                return notAPosition;
            }
        }

        public void clearPins()
        {
            Map.Pins.Clear();
        }

        public void AddPin(Registration registration, EventHandler evnt)
        {
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(registration.Latitude, registration.Longitude),
                Label = registration.Name,
                Address = registration.Description
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

        public async Task RequestLocationPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await App.Current.MainPage.DisplayAlert("Permissions", "Nous avons besoin de votre localisation pour ce service", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Permission refusée", "Impossible de continuer...", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Permission refusée", "Impossible de continuer...", "OK");
            }
        }
    }
}
