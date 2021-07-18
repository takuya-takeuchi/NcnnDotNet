using YoloV3.Services.Interfaces;

namespace YoloV3.iOS.Services
{

    public sealed class NativeService : INativeService
    {

        public int GetPrimeCount(int n)
        {
            return NativeSharp.Native.GetPrimeCount(n);
        }

    }

}
