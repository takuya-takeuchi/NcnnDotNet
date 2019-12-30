// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void SRand(uint seed)
        {
            NativeMethods.stdlib_srand(seed);
        }

        #endregion

    }

}
