using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Native.Nuget.Services.Interfaces;
using Xamarin.Native.Nuget.ViewModels.Interfaces;

namespace Xamarin.Native.Nuget.ViewModels
{

    public class MainPageViewModel : ViewModelBase,  IMainPageViewModel
    {

        #region Fields

        private readonly INativeService _NativeService;

        #endregion

        #region Constructors

        public MainPageViewModel(INavigationService navigationService,
                                 INativeService nativeService)
            : base(navigationService)
        {
            this._NativeService = nativeService;
            this.Title = "Main Page";
        }

        #endregion

        #region Properties

        private string _Title;

        public string Title
        {
            get => this._Title;
            private set => this.SetProperty(ref this._Title, value);
        }

        private int _Max;

        public int Max
        {
            get => this._Max;
            set => this.SetProperty(ref this._Max, value);
        }

        private int _Count;

        public int Count
        {
            get => this._Count;
            private set => this.SetProperty(ref this._Count, value);
        }

        private ICommand _CalcCommand;

        public ICommand CalcCommand
        {
            get
            {
                return this._CalcCommand ?? (this._CalcCommand = new DelegateCommand(() =>
                {
                    this.Count = this._NativeService.GetPrimeCount(this._Max);
                }));

            }
        }

        #endregion

    }

}