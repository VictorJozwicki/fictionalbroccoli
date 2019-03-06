using System;
using Prism.Commands;
using Prism.Navigation;
using fictionalbroccoli.Services;
using fictionalbroccoli.Models;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using System.Diagnostics;

namespace fictionalbroccoli.ViewModels
{
    public class BrocoRegisterAddViewModel : ViewModelBase
    {
        public INavigationService _navigationService;
        public IRegisterService _registerService;
        public IPictureService _pictureService;
        public IMapService _mapService;

        public Registration _registration;
        public Registration Registration
        {
            get { return _registration; }
            set { SetProperty(ref _registration, value); }
        }

        public DelegateCommand CommandAdd { get; private set; }
        public DelegateCommand CommandTakePicture { get; private set; }

        public ImageSource _imageSrc;
        public ImageSource ImageSrc
        {
            get { return _imageSrc; }
            set { SetProperty(ref _imageSrc, value); }
        }

        public BrocoRegisterAddViewModel(
            INavigationService navigationService, 
            IRegisterService registerService, 
            IPictureService pictureService,
            IMapService mapService) : base(navigationService)
        {
            Title = "Nouveau";

            _registerService = registerService;
            _navigationService = navigationService;
            _pictureService = pictureService;
            _mapService = mapService;

            CommandAdd = new DelegateCommand(HandleAdd);
            CommandTakePicture = new DelegateCommand(HandleTakePicture);

            Registration = new Registration();
        }

        private async void HandleAdd()
        {
            Registration.Date = DateTime.Now;
            var position = await _mapService.GetCurrentLocation();
            Registration.Latitude = position.Latitude;
            Registration.Longitude = position.Longitude;
            Debug.WriteLine(Registration.Latitude);

            _registerService.Add(Registration);
            await _navigationService.NavigateAsync("/AppliMenu/NavigationPage/BrocoRegister");
        }

        private async void HandleTakePicture()
        {
            MediaFile file = await _pictureService.TakeFromCamera();
            ImageSrc = ImageSource.FromStream(file.GetStream);
            Registration.ImagePath = file.Path;
        }
    }
}