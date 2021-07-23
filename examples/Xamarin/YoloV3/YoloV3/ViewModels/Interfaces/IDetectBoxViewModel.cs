using Xamarin.Forms;

namespace YoloV3.ViewModels.Interfaces
{

    public interface IDetectBoxViewModel
    {

        Rectangle Bounds
        {
            get;
        }

        int Height
        {
            get;
        }

        string Label
        {
            get;
        }

        float Prob
        {
            get;
        }

        int Width
        {
            get;
        }

        int X
        {
            get;
        }

        int Y
        {
            get;
        }

    }

}