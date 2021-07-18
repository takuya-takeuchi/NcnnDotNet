using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using YoloV3.Services;
using YoloV3.Services.Interfaces;
using YoloV3.ViewModels;
using YoloV3.Views;

namespace YoloV3
{

    public partial class App
    {

        #region Constructors

        public App() : this(null) { }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        #region Overrids

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }

        #endregion

        #endregion

    }

}
