using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using fictionalbroccoli.Models;
using fictionalbroccoli.Services;
using Xamarin.Forms;
using System.Diagnostics;

namespace fictionalbroccoli.ViewModels
{
    public class BrocoRegisterDetailViewModel : ViewModelBase
    {
        public INavigationService _navigationService;
        public IRegisterService _registerService;
        public IDialogService _dialogService;
        public Registration _registration;

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _date = "";
        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _tag = "";
        public string Tag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }

        public ImageSource _imageSrc;
        public ImageSource ImageSrc
        {
            get { return _imageSrc; }
            set { SetProperty(ref _imageSrc, value); }
        }

        public DelegateCommand CommandDelete { get; private set; }
        public DelegateCommand CommandGoUpdate { get; private set; }


        // Functions 
        public BrocoRegisterDetailViewModel(
            INavigationService navigationService,
            IRegisterService registerService,
            IDialogService dialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _registerService = registerService;
            _dialogService = dialogService;
            Title = "Enregistrement";
            CommandDelete =  new DelegateCommand(HandleDelete);
            CommandGoUpdate = new DelegateCommand(HandleGoUpdate);

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            _registration = parameters.GetValue<Registration>("Registration");
            Name = _registration.Name;
            Description = _registration.Description;
            Tag = _registration.Tag;
            ImageSrc = ImageSource.FromFile(_registration.ImagePath);
            // If today then output string today and test with Date.now == Date
            DateTime date = _registration.Date;
            Date = String.Concat("Photo prise le ", date.ToString("d"), " à ", date.ToString("t"));
            // On veut l'heure quand c'est aujourd'hui sinon la date

        }

        private void HandleDelete()
        {
            _dialogService.ShowMessage("Attention", "Êtes-vous sûr de vouloir supprimer ?", (res) =>
            {
                if (res)
                {
                    _registerService.Delete(_registration.Id);
                    _navigationService.GoBackAsync();
                }
            });

        }

        private async void HandleGoUpdate()
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Registration", _registration);
            await _navigationService.NavigateAsync("BrocoRegisterEdit", navigationParam);
        }
    }
}