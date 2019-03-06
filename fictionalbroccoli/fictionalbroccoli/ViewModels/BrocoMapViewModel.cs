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
using fictionalbroccoli.Services;
using fictionalbroccoli.Models;

namespace fictionalbroccoli.ViewModels
{
    public class BrocoMapViewModel : ViewModelBase
    {
        IMapService _mapService;
        IRegisterService _registerService;
        INavigationService _navigationService;

        private Map _map;
        public Map Map
        {
            get { return _map; }
            set { SetProperty(ref _map, value); }
        }

        public BrocoMapViewModel(
            INavigationService navigationService, 
            IMapService mapService,
            IRegisterService registerService
            ) : base(navigationService)
        {
            Title = "Carte";
            _navigationService = navigationService;
            _mapService = mapService;
            _registerService = registerService;

            CreateMap();
        }

        public async void CreateMap()
        {
            Map = _mapService.GetMap();

            //var position = await _mapService.GetCurrentLocation();
            //Debug.WriteLine(position);
            //var address = await _mapService.GetCurrentAddress(position);
            //Debug.WriteLine(address);

            var registrations = _registerService.GetAll();
            _mapService.clearPins();
            foreach (Registration registration in registrations)
            {
                var evnt = new EventHandler(async (object sender, EventArgs e) =>
                {
                    Debug.WriteLine("Event fired !");
                    var p = sender as Pin;
                    Debug.WriteLine(p);
                    var navigationParam = new NavigationParameters();
                    navigationParam.Add("Registration", registration);
                    await _navigationService.NavigateAsync("BrocoRegisterDetail", navigationParam);
                });
                _mapService.AddPin(registration.Latitude, registration.Longitude, registration.Name, evnt);
            }
        }
    }
}
