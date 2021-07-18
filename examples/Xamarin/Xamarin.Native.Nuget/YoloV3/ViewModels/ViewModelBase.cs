using Prism.Mvvm;
using Prism.Navigation;

namespace YoloV3.ViewModels
{

    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {

        #region Constructors

        public ViewModelBase(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        #endregion

        #region Properties

        protected INavigationService NavigationService
        {
            get;
        }

        #endregion

        #region Methods

        #region Overrids

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        #endregion

        #endregion

    }

}
