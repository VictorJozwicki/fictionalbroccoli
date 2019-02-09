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
        private string _sortText = "Sorting ?";

        public List<Registration> Registrations
        {
            get { return _registrations;  }
            set { SetProperty(ref _registrations, value);  }
        }

        public string SortText
        {
            get { return _sortText; }
            set { SetProperty(ref _sortText, value);  }
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
            SortText = "Sorting up";
            // Change the image to arrowUpSelected

        }

        private void HandleSortDown(Registration obj)
        {
            SortText = "Sorting down";
            // Change the image to arrowDownSelected
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Registrations = _registerService.GetAll(); // Gotta catch them all
        }
    }
}
