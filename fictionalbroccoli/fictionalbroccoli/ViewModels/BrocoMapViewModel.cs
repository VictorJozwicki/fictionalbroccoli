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

namespace fictionalbroccoli.ViewModels
{
    public class BrocoMapViewModel : ViewModelBase
    {
        IMapService _mapService;

        private Map _map;
        public Map Map
        {
            get { return _map; }
            set { SetProperty(ref _map, value); }
        }

        public BrocoMapViewModel(INavigationService navigationService, IMapService mapService) : base(navigationService)
        {
            Title = "Carte";
            _mapService = mapService;
            Debug.WriteLine(mapService.IsLocationAvailable());
            CreateMap();
        }

        public async void CreateMap()
        {
            Map = _mapService.GetMap();
            var position = await _mapService.GetCurrentLocation();
            Debug.WriteLine(position);
            _mapService.AddPin(position);
        }
    }
}
