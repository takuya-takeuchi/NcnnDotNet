using System;
using Prism.Commands;
using Prism.Navigation;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using YoloV3.Services.Interfaces;
using YoloV3.ViewModels.Interfaces;

namespace YoloV3.ViewModels
{

    public sealed class MainPageViewModel : ViewModelBase, IMainPageViewModel
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

        #region Properties

        private readonly Lazy<DelegateCommand> _FilePickCommand;

        private DelegateCommand FilePickCommandFactory()
        {
            return new DelegateCommand(async () =>
            {
                // var result = await FilePicker.PickAsync(new PickOptions
                // {
                //     PickerTitle = "Please select a image file to detect object",
                //     FileTypes = FilePickerFileType.Images
                // });

                // if (result == null) 
                //     return;

                var resourcePrefix = $"YoloV3.data.";
                var assembly = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(MainPageViewModel)).Assembly;
                var path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dog.jpg");
                var stream = assembly.GetManifestResourceStream(resourcePrefix + "dog.jpg");
                using (var fs = System.IO.File.Create(path))
                {
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    stream.CopyTo(fs);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                }

                var detectResult = this._DetectService.Detect(path);
                if (detectResult == null) 
                    return;

                // var stream = await result.OpenReadAsync();
                var surface = SKSurface.Create(new SKImageInfo(detectResult.Width, detectResult.Height, SKColorType.Rgba8888));
                using var paint = new SKPaint();
                using var bitmap = SKBitmap.Decode(path);

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