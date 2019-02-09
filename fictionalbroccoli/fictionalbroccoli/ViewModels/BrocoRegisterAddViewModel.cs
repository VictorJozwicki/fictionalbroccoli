using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using fictionalbroccoli.Services;
using fictionalbroccoli.Models;

namespace fictionalbroccoli.ViewModels
{
    public class BrocoRegisterAddViewModel : ViewModelBase
    {
        public INavigationService _navigationService;
        public IRegisterService _registerService;

        public Registration _registration;
        public Registration Registration
        {
            get { return _registration; }
            set { SetProperty(ref _registration, value); }
        }

        public DelegateCommand CommandAdd { get; private set; }

        public BrocoRegisterAddViewModel(INavigationService navigationService, IRegisterService registerService) : base(navigationService)
        {
            Title = "Nouveau";

            _registerService = registerService;
            _navigationService = navigationService;

            CommandAdd = new DelegateCommand(HandleAdd);

            Registration = new Registration();
        }

        private void HandleAdd()
        {
            _registerService.Add(Registration);
            _navigationService.NavigateAsync("/AppliMenu/NavigationPage/BrocoRegister");
        }
    }
}
