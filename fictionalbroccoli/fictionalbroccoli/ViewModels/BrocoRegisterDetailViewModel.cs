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

namespace fictionalbroccoli.ViewModels
{
    public class BrocoRegisterDetailViewModel : ViewModelBase, INavigatedAware
    {
        public INavigationService _navigationService;
        public IRegisterService _registerService;
        public IDialogService _dialogService;
        public Registration registration;

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

        public DelegateCommand CommandDelete { get; private set; }
        public DelegateCommand CommandSave { get; private set; }


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
            CommandSave = new DelegateCommand(HandleSave);

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            registration = parameters.GetValue<Registration>("Registration");
            Name = registration.Name;
            Description = registration.Description;
        }

        private void HandleDelete()
        {
            _dialogService.ShowMessage("Attention", "Êtes-vous sûr de vouoir supprimer ?", (res) =>
            {
                if (res)
                {
                    _registerService.Delete(registration.Id);
                    _navigationService.GoBackAsync();
                }
            });

        }

        private void HandleSave()
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Registration", registration);
            _navigationService.NavigateAsync("BrocoRegisterEdit", navigationParam);
        }
    }
}