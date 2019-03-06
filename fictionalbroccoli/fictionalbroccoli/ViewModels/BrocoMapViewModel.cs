using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Plugin.Geolocator;
using System.Diagnostics;

namespace fictionalbroccoli.ViewModels
{
    public class BrocoMapViewModel : ViewModelBase
    {
        private Map _map;
        public Map Map
        {
            get { return _map; }
            set { SetProperty(ref _map, value); }
        }

        public BrocoMapViewModel(INavigationService navigationService) : base(navigationService)
        {
            CreateMap();

            Debug.WriteLine(IsLocationAvailable());
        }

        public Map getMap(Position pos)
        {
            return new Map(MapSpan.FromCenterAndRadius(
                pos,
                Distance.FromMiles(0.3)))
            {
                //IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
        }

        public async void CreateMap()
        {
            var position = await GetCurrentLocation();
            Debug.WriteLine(position);
            Map = getMap(new Position(position.Latitude, position.Longitude));
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
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

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unable to get location: " + ex);
                }

                if (position == null)
                    return null;

                var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                        position.Timestamp, position.Latitude, position.Longitude,
                        position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

                Debug.WriteLine(output);

                return position;
            }

    }
}
