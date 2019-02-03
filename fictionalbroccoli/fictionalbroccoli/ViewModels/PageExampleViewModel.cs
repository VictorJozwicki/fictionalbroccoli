using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;

namespace fictionalbroccoli.ViewModels
{
    public class PageExampleViewModel : ViewModelBase
    {
        public PageExampleViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Example page";
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        // Naviguation functions
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }
    }
}
