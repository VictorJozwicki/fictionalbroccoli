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

namespace fictionalbroccoli.ViewModels
{
    public class BrocoRegisterDetailViewModel : ViewModelBase, INavigatedAware
    {
        public INavigationService _navigationService;
        public IRegisterService _registerService;
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
        public BrocoRegisterDetailViewModel(INavigationService navigationService, IRegisterService registerService) : base(navigationService)
        {
            _navigationService = navigationService;
            _registerService = registerService;
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
            _registerService.Delete(registration.Id);
            _navigationService.NavigateAsync("/AppliMenu/NavigationPage/MainPage"); // TODO Doesn't really work
        }

        private void HandleSave()
        {
            Title = "Saving";
            // TODO 
        }
    }
}