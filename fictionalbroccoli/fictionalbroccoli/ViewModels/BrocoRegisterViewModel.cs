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
    public class BrocoRegisterViewModel : ViewModelBase
    {
        //private IRegisterService _registerService;
        //private List<Registration> _registrations;
        //public List<Registration> Registrations
        //{
        //    get { return _registrations;  }
        //    set { SetProperty(ref _registrations, value);  }
        //}

        public BrocoRegisterViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Enregistrements";
            Registration temp = new Registration("title", "description");

            //_registerService = RegisterService;

            //_registerService.Add(temp);

            //_registrations = _registerService.GetAll();
        }
    }
}
