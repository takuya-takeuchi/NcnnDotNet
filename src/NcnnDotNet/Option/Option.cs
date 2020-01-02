using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class Option : NcnnObject
    {

        #region Constructors

        public Option()
        {
            var error = NativeMethods.option_Option_new(out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        internal Option(IntPtr ptr, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public bool UseVulkanCompute
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_vulkan_compute(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_vulkan_compute(this.NativePtr, value);
            }
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

            NativeMethods.option_Option_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}