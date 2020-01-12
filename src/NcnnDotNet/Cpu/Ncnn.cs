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

        public static PowerSave GetPowerSave()
        {
            var powerSave = NativeMethods.cpu_get_cpu_powersave();
            return (PowerSave)powerSave;
        }

        public static bool SetCpuPowerSave(PowerSave powerSave)
        {
            var ret = NativeMethods.cpu_set_cpu_powersave((int)powerSave);
            return ret == 0;
        }

        #endregion

    }

}