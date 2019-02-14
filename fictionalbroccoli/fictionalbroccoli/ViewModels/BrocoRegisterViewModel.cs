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
using System.Collections.ObjectModel;

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

        private ObservableCollection<Registration> _registrations;
        public ObservableCollection<Registration> Registrations
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
            Registrations = new ObservableCollection<Registration>(_registerService.GetAll()); // Gotta catch them all
            // Le plus simple c'est de rajouter un attribut DateText à Registration et ici de parcourir chacun pour leur appeler la fonction qui créer le DateText
            // item.DateText = DateTextString(item.Date);
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
                SortDown();
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
            Registrations = new ObservableCollection<Registration>(Registrations.OrderByDescending(i => i.Date));
        }

        private void SortDown()
        {
            Registrations = new ObservableCollection<Registration>(Registrations.Reverse());
        }

        // Returns a string with the DateText corresponding to the difference with the DateTime given
        private string DateTextString(DateTime date) // TODO: Il faut itérer chaque Registrations et prendre la date de chaque éléments et faire ça
        {
            string DateText = "il y a ";

            DateTime today = DateTime.Now;
            TimeSpan diff = today.Subtract(date);

            if (diff.Days >= 1)
                return String.Concat(DateText, diff.Days, "jours");
            else if (diff.Minutes >= 1)
                return String.Concat(DateText, diff.Minutes, " minutes");
            else
                return String.Concat(DateText, diff.Seconds, " secondes");
        }
    }
}
