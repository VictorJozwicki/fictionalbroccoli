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
    public class BrocoRegisterViewModel : ViewModelBase
    {
        public BrocoRegisterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Enregistrements";
        }
    }
}
