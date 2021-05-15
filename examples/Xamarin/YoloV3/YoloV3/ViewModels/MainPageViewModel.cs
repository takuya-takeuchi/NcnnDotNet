using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

using Prism.Commands;
using Prism.Navigation;
using YoloV3.Services.Interfaces;
using YoloV3.ViewModels.Interfaces;

namespace YoloV3.ViewModels
{

    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {

        #region Fields

        private readonly IDetectService _DetectService;

        #endregion

        #region Constructors

        public MainPageViewModel(INavigationService navigationService,
                                 IDetectService detectService)
            : base(navigationService)
        {
            this.Title = "Yolo V3";

            this._DetectService = detectService;
            this._FilePickCommand = new Lazy<DelegateCommand>(this.FilePickCommandFactory);
        }

        #endregion

        #region Constructors

        private readonly Lazy<DelegateCommand> _FilePickCommand;

        private DelegateCommand FilePickCommandFactory()
        {
            return new DelegateCommand(async () =>
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Please select a image file to detect object",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {

                    //Text = $"File Name: {result.FileName}";
                    var stream = await result.OpenReadAsync();
                    this._DetectService.Detect(result.FullPath);

                    this.SelectedImage = ImageSource.FromStream(() => stream);
                }
            });
        }

        public DelegateCommand FilePickCommand => this._FilePickCommand.Value;

        private ImageSource _SelectedImage;

        public ImageSource SelectedImage
        {
            get => this._SelectedImage;
            private set
            {
                this._SelectedImage = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

    }

}