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
    public class BrocoRegisterEditViewModel : ViewModelBase
    {
        public INavigationService _navigationService;
        public IRegisterService _registerService;

        public Registration _registration;
        public Registration Registration
        {
            get { return _registration; }
            set { SetProperty(ref _registration, value); }
        }

        public DelegateCommand CommandSave { get; private set; }

        public BrocoRegisterEditViewModel(INavigationService navigationService, IRegisterService registerService) : base(navigationService)
        {
            Title = "Edit Register";
            _navigationService = navigationService;
            _registerService = registerService;
            CommandSave = new DelegateCommand(HandleSave);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            Registration = parameters.GetValue<Models.Registration>("Registration");
        }

        private void HandleSave()
        {
            _registerService.Update(Registration);
            var navigationParam = new NavigationParameters();
            navigationParam.Add("Registration", Registration);
            _navigationService.GoBackAsync(navigationParam);
        }
    }
}
