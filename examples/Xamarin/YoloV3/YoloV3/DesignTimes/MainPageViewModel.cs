using Prism.Commands;
using Xamarin.Forms;
using YoloV3.ViewModels.Interfaces;

namespace YoloV3.DesignTimes
{

    public class MainPageViewModel : IMainPageViewModel
    {

        #region Constructors

        public MainPageViewModel()
        {
            this.Title = "Yolo V3";
        }

        #endregion

        #region Constructors

        public DelegateCommand FilePickCommand
        {
            get;
        }

        public ImageSource SelectedImage
        {
            get;
        }

        public string Title
        {
            get;
        }

        #endregion

    }

}