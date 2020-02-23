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

        public bool LightMode
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_lightmode(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_lightmode(this.NativePtr, value);
            }
        }

        public int NumThreads
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_num_threads(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_num_threads(this.NativePtr, value);
            }
        }

        public bool UseFP16Arithmetic
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_fp16_arithmetic(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_fp16_arithmetic(this.NativePtr, value);
            }
        }

        public bool UseFP16Packed
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_fp16_packed(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_fp16_packed(this.NativePtr, value);
            }
        }

        public bool UseFP16Storage
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_fp16_storage(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_fp16_storage(this.NativePtr, value);
            }
        }

        public bool UseInt8Arithmetic
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_int8_arithmetic(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_int8_arithmetic(this.NativePtr, value);
            }
        }

        public bool UseInt8Inference
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_int8_inference(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_int8_inference(this.NativePtr, value);
            }
        }

        public bool UseInt8Storage
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_int8_storage(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_int8_storage(this.NativePtr, value);
            }
        }

        public bool UsePackingLayout
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_packing_layout(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_packing_layout(this.NativePtr, value);
            }
        }

        public bool UseSgemmConvolution
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_sgemm_convolution(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_sgemm_convolution(this.NativePtr, value);
            }
        }

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

        public bool UseWinogradConvolution
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_use_winograd_convolution(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_set_use_winograd_convolution(this.NativePtr, value);
            }
        }

        public Allocator BlobAllocator
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_blob_allocator(this.NativePtr, out var value);

                if (NativeMethods.allocator_Allocator_dynamic_cast(value, out var type))
                {
                    return Allocator.GetAllocator(value, type);
                }

                return null;
            }
            set
            {
                this.ThrowIfDisposed();
                value?.ThrowIfDisposed();
                NativeMethods.option_Option_set_blob_allocator(this.NativePtr, value?.NativePtr ?? IntPtr.Zero);
            }
        }

        public Allocator WorkspaceAllocator
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_workspace_allocator(this.NativePtr, out var value);

                if (NativeMethods.allocator_Allocator_dynamic_cast(value, out var type))
                {
                    return Allocator.GetAllocator(value, type);
                }

                return null;
            }
            set
            {
                this.ThrowIfDisposed();
                value?.ThrowIfDisposed();
                NativeMethods.option_Option_set_workspace_allocator(this.NativePtr, value?.NativePtr ?? IntPtr.Zero);
            }
        }

        public VkAllocator BlobVkAllocator
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_blob_vkallocator(this.NativePtr, out var value);

                if (NativeMethods.allocator_VkAllocator_dynamic_cast(value, out var type))
                {
                    return VkAllocator.GetAllocator(value, type);
                }

                return null;
            }
            set
            {
                this.ThrowIfDisposed();
                value?.ThrowIfDisposed();
                NativeMethods.option_Option_set_blob_vkallocator(this.NativePtr, value?.NativePtr ?? IntPtr.Zero);
            }
        }

        public VkAllocator StagingVkAllocator
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_staging_vkallocator(this.NativePtr, out var value);

                if (NativeMethods.allocator_VkAllocator_dynamic_cast(value, out var type))
                {
                    return VkAllocator.GetAllocator(value, type);
                }

                return null;
            }
            set
            {
                this.ThrowIfDisposed();
                value?.ThrowIfDisposed();
                NativeMethods.option_Option_set_staging_vkallocator(this.NativePtr, value?.NativePtr ?? IntPtr.Zero);
            }
        }

        public VkAllocator WorkspaceVkAllocator
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.option_Option_get_workspace_vkallocator(this.NativePtr, out var value);

                if (NativeMethods.allocator_VkAllocator_dynamic_cast(value, out var type))
                {
                    return VkAllocator.GetAllocator(value, type);
                }

                return null;
            }
            set
            {
                this.ThrowIfDisposed();
                value?.ThrowIfDisposed();
                NativeMethods.option_Option_set_workspace_vkallocator(this.NativePtr, value?.NativePtr ?? IntPtr.Zero);
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