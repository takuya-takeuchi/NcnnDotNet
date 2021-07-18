using Xamarin.Forms;
using YoloV3.ViewModels.Interfaces;

namespace YoloV3.ViewModels
{

    public sealed class DetectBoxViewModel : IDetectBoxViewModel
    {

        #region Constructors

        public DetectBoxViewModel(float scale, Models.Object obj)
        {
            this.X = (int)obj.Rect.X;
            this.Y = (int)obj.Rect.Y;
            this.Width = (int)obj.Rect.Width;
            this.Height = (int)obj.Rect.Height;
            this.Label = "test";
            this.Prob = obj.Prob;

            this.Bounds = new Rectangle(obj.Rect.X * scale, obj.Rect.Y * scale, obj.Rect.Width * scale, obj.Rect.Height * scale);
        }

        #endregion

        #region Properties
        
        
        public Rectangle Bounds
        {
            get;
        }


        public int Height
        {
            get;
        }

        public string Label
        {
            get;
        }

        public float Prob
        {
            get;
        }

        public int Width
        {
            get;
        }

        public int X
        {
            get;
        }

        public int Y
        {
            get;
        }

        #endregion

    }

}