using Prism;
using Prism.Ioc;
using fictionalbroccoli.ViewModels;
using fictionalbroccoli.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fictionalbroccoli.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace fictionalbroccoli
{   
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("AppliMenu/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();


            // Pages
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PageExample, PageExampleViewModel>();
            containerRegistry.RegisterForNavigation<BrocoMap, BrocoMapViewModel>();
            containerRegistry.RegisterForNavigation<BrocoRegister, BrocoRegisterViewModel>();
            containerRegistry.RegisterForNavigation<BrocoNew, BrocoNewViewModel>();
            containerRegistry.RegisterForNavigation<BrocoBonus, BrocoBonusViewModel>();

            // Menu
            containerRegistry.RegisterForNavigation<AppliMenu, AppliMenuViewModel>();

            // Services
            containerRegistry.RegisterSingleton<IRegisterService, RegisterService>();
        }
    }
}
