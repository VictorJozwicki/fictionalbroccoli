using System;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using fictionalbroccoli.Models;
using fictionalbroccoli.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

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
        public DelegateCommand<String> CommandTag { get; private set; }


        private ObservableCollection<Registration> _registrations;
        public ObservableCollection<Registration> Registrations
        {
            get { return _registrations; }
            set { SetProperty(ref _registrations, value); }
        }

        private ObservableCollection<String> _tags;
        public ObservableCollection<String> Tags
        {
            get { return _tags; }
            set { SetProperty(ref _tags, value); }
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

        private List<String> _filterTagList = new List<string>();
        public List<String> FilterTagList
        {
            get { return _filterTagList; }
            set { SetProperty(ref _filterTagList, value); }
        }

        // Functions
        public BrocoRegisterViewModel(INavigationService navigationService, IRegisterService registerService) : base(navigationService)
        {
            Title = "Enregistrements";
            CommandGoDetail = new DelegateCommand<Registration>(HandleDetail);
            CommandSort = new DelegateCommand<Registration>(HandleSort);
            CommandTag = new DelegateCommand<string>(HandleTag);

            _navigationService = navigationService;
            _registerService = registerService;

            Tags = new ObservableCollection<String>() { "#factorio", "#drink", "#videogame", "#drink", "#videogame", "#drink", "#videogame", "#drink", "#videogame", "#drink", "#videogame" };
        }



        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            InitOrReset();


        }

        // Handlers
        private void HandleDetail(Registration selectedRegistration)
        {
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Registration", selectedRegistration);
            _navigationService.NavigateAsync("BrocoRegisterDetail", navigationParam);
        }

        private void HandleTag(string text)
        {
            Console.WriteLine(text);
            if(FilterTagList.Contains(text)) // If it contains, it deletes
            {
                Console.WriteLine("Contains " + text + "deleting");
                FilterTagList.Remove(text);
            }
            else // First time
            {
                Console.WriteLine("Does not contain " + text + ", adding");
                FilterTagList.Add(text);
                // Filter it here (use linq maybe or something)
            }

            // Filter the list
            if(!FilterTagList.Any())
            {
                Console.WriteLine("Empty list, resetting");
                InitOrReset();
            }
        }

        private void InitOrReset()
        {
            Registrations = new ObservableCollection<Registration>(_registerService.GetAll());
            foreach (Registration registration in Registrations)
            {
                registration.DateText = ConvertDateToHumanText(registration.Date);
                if (registration.ImagePath == null)
                    registration.ImagePath = "greenbox.png";
            }
            SortUp();
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
        private string ConvertDateToHumanText(DateTime date)
        {
            string DateText = "il y a ";

            DateTime today = DateTime.Now;
            TimeSpan diff = today.Subtract(date);

            Console.WriteLine(diff.Days % 30);

            if (diff.Days / 365 >= 1)
                return String.Concat(DateText, diff.Days / 365, " ans");
            else if (diff.Days / 30 >= 1)
                return String.Concat(DateText, diff.Days / 30, " mois");
            else if (diff.Days >= 1)
                return String.Concat(DateText, diff.Days, " jours");
            else if (diff.Hours >= 1)
                return String.Concat(DateText, diff.Hours, " heures");
            else if (diff.Minutes >= 1)
                return String.Concat(DateText, diff.Minutes, " minutes");
            else
                return String.Concat(DateText, diff.Seconds, " secondes");
        }
    }
}
