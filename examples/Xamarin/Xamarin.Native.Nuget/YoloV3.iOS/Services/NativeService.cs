using Xamarin.Native.Nuget.Services.Interfaces;

namespace Xamarin.Native.Nuget.iOS.Services
{

    public sealed class NativeService : INativeService
    {

        public int GetPrimeCount(int n)
        {
            return NativeSharp.Native.GetPrimeCount(n);
        }

    }

}
