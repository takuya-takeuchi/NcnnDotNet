using YoloV3.Models;

namespace YoloV3.Services.Interfaces
{

    public interface IDetectService
    {

        DetectResult Detect(string file);

    }

}