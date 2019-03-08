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
        public IDialogService _dialogService;

        public string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string _tag;
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

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
            IMapService mapService,
            IDialogService dialogService) : base(navigationService)
        {
            Title = "Nouveau";

            _registerService = registerService;
            _navigationService = navigationService;
            _pictureService = pictureService;
            _mapService = mapService;
            _dialogService = dialogService;

            CommandAdd = new DelegateCommand(HandleAdd, CanHandleAdd)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => Description)
                .ObservesProperty(() => Tag);
            CommandTakePicture = new DelegateCommand(HandleTakePicture);

            Registration = new Registration();
        }

        private void HandleAdd()
        {
            _dialogService.ShowMessage("Attention", "Êtes-vous sûr de vouloir ajouter cet item ?", async (res) =>
            {
                if (res)
                {
                    Registration.Name = Name;
                    Registration.Description = Description;
                    Registration.Tag = Tag;
                    Registration.Date = DateTime.Now;
                    var position = await _mapService.GetCurrentLocation();
                    Registration.Latitude = position.Latitude;
                    Registration.Longitude = position.Longitude;

                    var address = await _mapService.GetCurrentAddress(position);
                    Debug.WriteLine(address);
                    Registration.Address = address;

                    _registerService.Add(Registration);
                    await _navigationService.NavigateAsync("/AppliMenu/NavigationPage/BrocoRegister");
                }
            });
         }

        private Boolean CanHandleAdd()
        {
            Debug.WriteLine(string.IsNullOrEmpty(Name));

            return !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(Description)
            && !string.IsNullOrEmpty(Tag);
        }

        private async void HandleTakePicture()
        {
            MediaFile file = await _pictureService.PickOrTakePhotoAsync();
            ImageSrc = ImageSource.FromStream(file.GetStream);
            Registration.ImagePath = file.Path;
        }
    }
}