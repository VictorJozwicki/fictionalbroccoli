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
        private string _homeBckColor = "Silver", _mapBckColor = "Transparent", _registerBckColor = "Transparent", _addBckColor = "Transparent", _bonusBckColor = "Transparent";

        public string HomeBckColor
        {
            get { return _homeBckColor; }
            set { SetProperty(ref _homeBckColor, value); }
        }

        public string MapBckColor
        {
            get { return _mapBckColor; }
            set { SetProperty(ref _mapBckColor, value); }
        }

        public string RegisterBckColor
        {
            get { return _registerBckColor; }
            set { SetProperty(ref _registerBckColor, value); }
        }

        public string AddBckColor
        {
            get { return _addBckColor; }
            set { SetProperty(ref _addBckColor, value); }
        }

        public string BonusBckColor
        {
            get { return _bonusBckColor; }
            set { SetProperty(ref _bonusBckColor, value); }
        }

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
            ResetNavColor();
            HomeBckColor = "Silver";
            _navigationService.NavigateAsync("NavigationPage/MainPage");
        }

        // Application
        void HandleMap()
        {
            ResetNavColor();
            MapBckColor = "Silver";
            _navigationService.NavigateAsync("NavigationPage/BrocoMap");
        }

        void HandleRegister()
        {
            ResetNavColor();
            RegisterBckColor = "Silver";
            _navigationService.NavigateAsync("NavigationPage/BrocoRegister");
        }

        void HandleNew()
        {
            ResetNavColor();
            AddBckColor = "Silver";
            _navigationService.NavigateAsync("NavigationPage/BrocoRegisterAdd");
        }

        // Bonus
        void HandleBonus()
        {
            ResetNavColor();
            BonusBckColor = "Silver";
            _navigationService.NavigateAsync("NavigationPage/BrocoBonus");
        }

        void ResetNavColor()
        {
            HomeBckColor = "Transparent";
            MapBckColor = "Transparent";
            RegisterBckColor = "Transparent";
            AddBckColor = "Transparent";
            BonusBckColor = "Transparent";


        }
    }
}
