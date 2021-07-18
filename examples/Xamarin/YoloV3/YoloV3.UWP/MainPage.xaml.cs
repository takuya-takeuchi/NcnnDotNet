using Prism;
using Prism.Ioc;
using YoloV3.Services;
using FileAccessService = YoloV3.UWP.Services.FileAccessService;

namespace YoloV3.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new YoloV3.App(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterSingleton<IFileAccessService, FileAccessService>();
        }
    }
}
