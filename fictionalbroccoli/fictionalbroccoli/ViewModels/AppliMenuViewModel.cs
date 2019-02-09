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
        public DelegateCommand CommandGoHome { get; private set; }
        public DelegateCommand CommandGoAppMap { get; private set; }
        public DelegateCommand CommandGoAppRegister { get; private set; }
        public DelegateCommand CommandGoAppNew { get; private set; }
        public DelegateCommand CommandGoBonus { get; private set; }

        public AppliMenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // Home
            CommandGoHome = new DelegateCommand(HandleHome);
            // Application
            CommandGoAppMap = new DelegateCommand(HandleMap);
            CommandGoAppRegister = new DelegateCommand(HandleRegister);
            CommandGoAppNew = new DelegateCommand(HandleNew);
            // Bonus
            CommandGoBonus = new DelegateCommand(HandleBonus);
        }

        // Home
        void HandleHome()
        {
            _navigationService.NavigateAsync("NavigationPage/MainPage");
        }

        // Application
        void HandleMap()
        {
            _navigationService.NavigateAsync("NavigationPage/BrocoMap");
        }

        void HandleRegister()
        {
            _navigationService.NavigateAsync("NavigationPage/BrocoRegister");
        }

        void HandleNew()
        {
            _navigationService.NavigateAsync("NavigationPage/BrocoRegisterAdd");
        }

        // Bonus
        void HandleBonus()
        {
            _navigationService.NavigateAsync("NavigationPage/BrocoBonus");
        }
    }
}
