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
    public class BrocoRegisterViewModel : ViewModelBase
    {
        private IRegisterService _registerService;
        public INavigationService _navigationService;
        public DelegateCommand<Registration> CommandGoDetail { get; private set; }
        public DelegateCommand<Registration> CommandSortUp { get; private set; }
        public DelegateCommand<Registration> CommandSortDown { get; private set; }
        private List<Registration> _registrations;
        private string _arrowUp = "arrowUp", _arrowDown = "arrowDown";

        public List<Registration> Registrations
        {
            get { return _registrations;  }
            set { SetProperty(ref _registrations, value);  }
        }

        public string ArrowUp
        {
            get { return _arrowUp; }
            set { SetProperty(ref _arrowUp, value); }
        }

        public string ArrowDown
        {
            get { return _arrowDown; }
            set { SetProperty(ref _arrowDown, value); }
        }

        public BrocoRegisterViewModel(INavigationService navigationService, IRegisterService registerService) : base(navigationService)
        {
            Title = "Enregistrements";
            CommandGoDetail = new DelegateCommand<Registration>(HandleDetail);
            CommandSortUp = new DelegateCommand<Registration>(HandleSortUp);
            CommandSortDown = new DelegateCommand<Registration>(HandleSortDown);


            _navigationService = navigationService;
            _registerService = registerService; // Local service
            Registrations = _registerService.GetAll(); // Gotta catch them all

        }

        private void HandleDetail(Registration selectedRegistration)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Registration", selectedRegistration);
            _navigationService.NavigateAsync("BrocoRegisterDetail", navigationParam);
        }

        private void HandleSortUp(Registration obj)
        {
            ArrowUp = "arrowUpSelected";
            ArrowDown = "arrowDown";
            // TODO Sort up

        }

        private void HandleSortDown(Registration obj)
        {
            ArrowUp = "arrowUp";
            ArrowDown = "arrowDownSelected";
            // TODO Sort down
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Registrations = _registerService.GetAll(); // Gotta catch them all
        }
    }
}
