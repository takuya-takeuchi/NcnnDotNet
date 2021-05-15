using System;
using Xamarin.Essentials;
using Xamarin.Forms;

using Prism.Commands;
using Prism.Navigation;
using YoloV3.ViewModels.Interfaces;

namespace YoloV3.ViewModels
{

    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {

        #region Constructors

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Yolo V3";

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

                //Text = $"File Name: {result.FileName}";
                var stream = await result.OpenReadAsync();
                this.SelectedImage = ImageSource.FromStream(() => stream);
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