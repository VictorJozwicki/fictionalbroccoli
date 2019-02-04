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
    public class BrocoNewViewModel : ViewModelBase
    {
        public BrocoNewViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Nouveau";
        }
    }
}
