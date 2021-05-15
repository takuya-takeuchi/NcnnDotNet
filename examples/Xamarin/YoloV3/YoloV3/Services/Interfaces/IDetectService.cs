using System.Collections.Generic;
using YoloV3.Models;

namespace YoloV3.Services.Interfaces
{

    public interface IDetectService
    {

        IEnumerable<Object> Detect(string file);

    }

}