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
        public DelegateCommand<Registration> CommandSort { get; private set; }

        private List<Registration> _registrations;
        public List<Registration> Registrations
        {
            get { return _registrations;  }
            set { SetProperty(ref _registrations, value);  }
        }

        // Sorting
        private string _sortText = "Plus récent", _sortImageSource = "lightUpArrow";
        public string SortText
        {
            get { return _sortText; }
            set { SetProperty(ref _sortText, value); }
        }

        public string SortImageSource
        {
            get { return _sortImageSource; }
            set { SetProperty(ref _sortImageSource, value); }
        }

        private bool _sortRecent = true;
        public bool SortRecent
        {
            get { return _sortRecent; }
            set { SetProperty(ref _sortRecent, value); }
        }

        // Functions
        public BrocoRegisterViewModel(INavigationService navigationService, IRegisterService registerService) : base(navigationService)
        {
            Title = "Enregistrements";
            CommandGoDetail = new DelegateCommand<Registration>(HandleDetail);
            CommandSort = new DelegateCommand<Registration>(HandleSort);



            _navigationService = navigationService;
            _registerService = registerService; // Local service
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Registrations = _registerService.GetAll(); // Gotta catch them all
            this.SortUp();
        }


        // Handlers
        private void HandleDetail(Registration selectedRegistration)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Registration", selectedRegistration);
            _navigationService.NavigateAsync("BrocoRegisterDetail", navigationParam);
        }

        private void HandleSort(Registration obj)
        {
            if(SortRecent)
            {
                SortRecent = false;
                SortText = "Plus ancien";
                SortImageSource = "lightDownArrow";
                Registrations = SortDown();
            }
            else
            {
                SortRecent = true;
                SortText = "Plus récent";
                SortImageSource = "lightUpArrow";
                SortUp();
            }
        }

        private void SortUp()
        {
            Registrations.Sort((x, y) => DateTime.Compare(y.Date, x.Date)); // Fonctionne tout seul
        }

        private List<Registration> SortDown()
        {
            Registrations.Sort((x, y) => DateTime.Compare(x.Date, y.Date)); // Fonctionne tout seul
            return Registrations;
        }
    }
}
