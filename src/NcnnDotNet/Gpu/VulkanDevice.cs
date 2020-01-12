using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VulkanDevice : NcnnObject
    {

        #region Constructors
        
        public VulkanDevice(int deviceIndex)
        {
            var error = NativeMethods.gpu_VulkanDevice_new(deviceIndex, out var device);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = device;
        }

        internal VulkanDevice(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.gpu_VulkanDevice_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}