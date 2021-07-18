using System;
using Prism.Commands;
using Prism.Navigation;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using YoloV3.Services;
using YoloV3.Services.Interfaces;
using YoloV3.ViewModels.Interfaces;

namespace YoloV3.ViewModels
{

    public sealed class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {

        #region Fields

        private readonly IFileAccessService _FileAccessService;

        private readonly IDetectService _DetectService;

        #endregion

        #region Constructors

        public MainPageViewModel(INavigationService navigationService,
                                 IFileAccessService fileAccessService,
                                 IDetectService detectService)
            : base(navigationService)
        {
            this.Title = "Yolo V3";

            this._FileAccessService = fileAccessService;
            this._DetectService = detectService;
            this._FilePickCommand = new Lazy<DelegateCommand>(this.FilePickCommandFactory);
        }

        #endregion

        #region Properties

        private readonly Lazy<DelegateCommand> _FilePickCommand;

        private DelegateCommand FilePickCommandFactory()
        {
            return new DelegateCommand(async () =>
            {
                var result = await this._FileAccessService.GetFileContent();
                if (result == null) 
                    return;

                var detectResult = this._DetectService.Detect(result);
                if (detectResult == null) 
                    return;

                var surface = SKSurface.Create(new SKImageInfo(detectResult.Width, detectResult.Height, SKColorType.Rgba8888));
                using var paint = new SKPaint();
                using var bitmap = SKBitmap.Decode(result);

                surface.Canvas.DrawBitmap(bitmap, 0, 0, paint);
                paint.StrokeWidth = 3;
                paint.TextSize = 48;
                paint.IsAntialias = true;
                paint.TextEncoding = SKTextEncoding.Utf8;

                string[] classNames =
                {
                    "background",
                    "aeroplane",
                    "bicycle",
                    "bird",
                    "boat",
                    "bottle",
                    "bus",
                    "car",
                    "cat",
                    "chair",
                    "cow",
                    "diningtable",
                    "dog",
                    "horse",
                    "motorbike",
                    "person",
                    "pottedplant",
                    "sheep",
                    "sofa",
                    "train",
                    "tvmonitor"
                };

                foreach (var box in detectResult.Boxes)
                {
                    paint.Color = SKColors.Red;
                    paint.Style = SKPaintStyle.Stroke;
                    surface.Canvas.DrawRect(box.Rect.X, box.Rect.Y, box.Rect.Width, box.Rect.Height, paint);

                    paint.Color = SKColors.Black;
                    paint.Style = SKPaintStyle.Fill;
                    surface.Canvas.DrawText(classNames[box.Label], new SKPoint(box.Rect.X + 5, box.Rect.Y + 5), paint);
                }
                this.SelectedImage = ImageSource.FromStream(() => surface.Snapshot().Encode().AsStream());
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