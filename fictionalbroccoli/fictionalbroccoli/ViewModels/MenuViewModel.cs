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
    public class MenuViewModel : ViewModelBase
    {
        public INavigationService _navigationService;
        public DelegateCommand CommandGoB;
        public MenuViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            CommandGoB = new DelegateCommand(HandleAction);

        }

        void HandleAction()
        {
            _navigationService.NavigateAsync("NavigationPage/PageExample");
        }

    }
}
