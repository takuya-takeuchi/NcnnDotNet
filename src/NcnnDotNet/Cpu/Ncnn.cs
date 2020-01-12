// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    /// <summary>
    /// Provides the methods of ncnn.
    /// </summary>
    public static partial class Ncnn
    {

        #region Methods

        public static int GetCpuCount()
        {
            return NativeMethods.cpu_get_cpu_count();
        }

        public static PowerSave SetCpuPowerSave()
        {
            var powerSave = NativeMethods.cpu_get_cpu_powersave();
            return (PowerSave)powerSave;
        }

        public static bool SetCpuPowerSave(PowerSave powerSave)
        {
            var ret = NativeMethods.cpu_set_cpu_powersave((int)powerSave);
            return ret == 0;
        }

        public static PowerSave GetOmpNumThreads()
        {
            var powerSave = NativeMethods.cpu_get_omp_num_threads();
            return (PowerSave)powerSave;
        }

        public static void SetOmpNumThreads(int numThreads)
        {
            NativeMethods.cpu_set_omp_num_threads(numThreads);
        }

        public static int GetOmpDynamic()
        {
            return NativeMethods.cpu_get_omp_dynamic();
        }

        public static void SetOmpDynamic(int dynamic)
        {
            NativeMethods.cpu_set_omp_dynamic(dynamic);
        }

        #endregion

    }

}