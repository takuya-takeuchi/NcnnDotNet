using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public class Layer : NcnnObject
    {

        #region Constructors

        //public Layer()
        //{
        //    var error = NativeMethods.layer_Layer_new(out var ret);
        //    if (error != NativeMethods.ErrorType.OK)
        //        throw new NcnnException("Unknown Exception");

        //    this.NativePtr = ret;
        //}

        protected Layer()
        { }

        internal Layer(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public bool OneBlobOnly
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.layer_Layer_get_one_blob_only(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.layer_Layer_set_one_blob_only(this.NativePtr, value);
            }
        }

        public bool SupportPacking
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.layer_Layer_get_support_packing(this.NativePtr, out var value);
                return value;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.layer_Layer_set_support_packing(this.NativePtr, value);
            }
        }

        public VulkanDevice VkDev
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.layer_Layer_get_vkdev(this.NativePtr, out var value);
                return new VulkanDevice(value, false);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.layer_Layer_set_vkdev(this.NativePtr, value.NativePtr);
            }
        }

        #endregion

        #region Methods

        public int CreatePipeline(Option option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_create_pipeline(this.NativePtr, option.NativePtr, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int DestroyPipeline(Option option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_destroy_pipeline(this.NativePtr, option.NativePtr, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int Forward(Mat bottomBlob, Mat topBlob, Option option)
        {
            if (bottomBlob == null)
                throw new ArgumentNullException(nameof(bottomBlob));
            if (topBlob == null)
                throw new ArgumentNullException(nameof(topBlob));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            bottomBlob.ThrowIfDisposed();
            topBlob.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_forward2(this.NativePtr,
                                                           bottomBlob.NativePtr,
                                                           topBlob.NativePtr,
                                                           option.NativePtr,
                                                           out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int Forward(VkMat bottomBlob, VkMat topBlob, VkCompute compute, Option option)
        {
            if (bottomBlob == null)
                throw new ArgumentNullException(nameof(bottomBlob));
            if (topBlob == null)
                throw new ArgumentNullException(nameof(topBlob));
            if (compute == null)
                throw new ArgumentNullException(nameof(compute));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            bottomBlob.ThrowIfDisposed();
            topBlob.ThrowIfDisposed();
            compute.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_forward2_vkmat(this.NativePtr,
                                                                 bottomBlob.NativePtr,
                                                                 topBlob.NativePtr,
                                                                 compute.NativePtr,
                                                                 option.NativePtr,
                                                                 out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int ForwardInplace(Mat mat, Option option)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_forward_inplace(this.NativePtr,
                                                                  mat.NativePtr,
                                                                  option.NativePtr,
                                                                  out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int LoadModel(ModelBin modelBin)
        {
            if (modelBin == null)
                throw new ArgumentNullException(nameof(modelBin));

            this.ThrowIfDisposed();
            modelBin.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_load_model(this.NativePtr, modelBin.NativePtr, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int LoadParam(ParamDict paramDict)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            this.ThrowIfDisposed();
            paramDict.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_load_param(this.NativePtr, paramDict.NativePtr, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int UploadModel(VkTransfer command, Option option)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            command.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_upload_model(this.NativePtr, command.NativePtr, option.NativePtr, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.layer_Layer_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}