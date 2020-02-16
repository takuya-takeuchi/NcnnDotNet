using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class GpuInfo : NcnnObject
    {

        #region Constructors

        internal GpuInfo(IntPtr ptr, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public bool SupportFP16Packed
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_get_support_fp16_packed(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_set_support_fp16_packed(this.NativePtr, value);
            }
        }

        public bool SupportFP16Storage
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_get_support_fp16_storage(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_set_support_fp16_storage(this.NativePtr, value);
            }
        }

        public bool SupportFP16Arithmetic
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_get_support_fp16_arithmetic(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_set_support_fp16_arithmetic(this.NativePtr, value);
            }
        }

        public bool SupportInt8Storage
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_get_support_int8_storage(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_set_support_int8_storage(this.NativePtr, value);
            }
        }

        public bool SupportInt8Arithmetic
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_get_support_int8_arithmetic(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.gpu_GpuInfo_set_support_int8_arithmetic(this.NativePtr, value);
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

            //NativeMethods.gpu_VulkanDevice_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}