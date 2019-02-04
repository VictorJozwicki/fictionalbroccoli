using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Prism.Mvvm;

namespace fictionalbroccoli.ViewModels
{
    public class AppliMenuViewModel : BindableBase
    {
        public INavigationService _navigationService;
        public DelegateCommand CommandGoExample { get; private set; }
        public DelegateCommand CommandGoHome { get; private set; }
        public AppliMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CommandGoExample = new DelegateCommand(HandleAction);
            CommandGoHome = new DelegateCommand(HandleHome);
        }

        void HandleAction()
        {
            _navigationService.NavigateAsync("NavigationPage/PageExample");
        }

        void HandleHome()
        {
            _navigationService.NavigateAsync("NavigationPage/MainPage");
        }
    }
}
