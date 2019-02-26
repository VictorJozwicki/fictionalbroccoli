using Prism.Commands;
using Prism.Navigation;
using fictionalbroccoli.Models;
using fictionalbroccoli.Services;
using System.Diagnostics;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace fictionalbroccoli.ViewModels
{
    public class BrocoRegisterEditViewModel : ViewModelBase
    {
        public INavigationService _navigationService;
        public IRegisterService _registerService;
        public IPictureService _pictureService;

        public Registration _registration;
        public Registration Registration
        {
            get { return _registration; }
            set { SetProperty(ref _registration, value); }
        }

        public ImageSource _imageSrc;
        public ImageSource ImageSrc
        {
            get { return _imageSrc; }
            set { SetProperty(ref _imageSrc, value); }
        }

        public DelegateCommand CommandUpdate { get; private set; }
        public DelegateCommand CommandTakePicture { get; private set; }

        public BrocoRegisterEditViewModel(INavigationService navigationService, IRegisterService registerService, IPictureService pictureService) : base(navigationService)
        {
            Title = "Edit Register";
            _navigationService = navigationService;
            _registerService = registerService;
            _pictureService = pictureService;
            CommandTakePicture = new DelegateCommand(HandleTakePicture);
            CommandUpdate = new DelegateCommand(HandleUpdate);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            Registration = parameters.GetValue<Models.Registration>("Registration");
            ImageSrc = ImageSource.FromFile(Registration.ImagePath);
        }

        private async void HandleTakePicture()
        {
            Debug.WriteLine("ok");
            MediaFile file = await _pictureService.TakeFromCamera();
            ImageSrc = ImageSource.FromStream(file.GetStream);
            Registration.ImagePath = file.Path;
        }

        private void HandleUpdate()
        {
            _registerService.Update(Registration);
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Registration", Registration);
            _navigationService.GoBackAsync(navigationParam);
        }
    }
}
