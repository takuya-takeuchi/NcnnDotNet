using System.Windows.Input;

namespace YoloV3.ViewModels.Interfaces
{

    public interface IMainPageViewModel
    {

        string Title
        {
            get;
        }

        int Max
        {
            get;
            set;
        }

        int Count
        {
            get;
        }

        ICommand CalcCommand
        {
            get;
        }

    }

}
