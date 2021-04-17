using NcnnDotNet;

namespace TestUtil
{

    public sealed class GlobalGpuInstance
    {

        #region Constructors

        public GlobalGpuInstance()
        {
            if (Ncnn.IsSupportVulkan)
                Ncnn.CreateGpuInstance();
        }

        ~GlobalGpuInstance()
        {
            if (Ncnn.IsSupportVulkan)
                Ncnn.DestroyGpuInstance();
        }

        #endregion

    }

}
