// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    /// <summary>
    /// Provides the methods of ncnn.
    /// </summary>
    public static partial class Ncnn
    {

        #region Methods
        
        public static double GetCurrentTime()
        {
            return NativeMethods.benchmark_get_current_time();
        }

        #endregion

    }

}