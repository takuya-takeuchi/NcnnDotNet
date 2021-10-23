using System;
using System.IO;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class Net : NcnnObject
    {

        #region Constructors

        public Net()
        {
            var error = NativeMethods.net_Net_new(out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        internal Net(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public Option Opt
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.net_Net_get_opt(this.NativePtr, out var opt);
                return new Option(opt, false);
            }
            set
            {
                this.ThrowIfDisposed();
                value?.ThrowIfDisposed();
                NativeMethods.net_Net_set_opt(this.NativePtr, value?.NativePtr ?? IntPtr.Zero);
            }
        }

        #endregion

        #region Methods

        public Extractor CreateExtractor()
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.net_Net_create_extractor(this.NativePtr, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Extractor(ret);
        }

        public VulkanDevice GetVulkanDevice()
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.net_Net_get_vulkan_device(this.NativePtr, out var device);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new VulkanDevice(device);
        }

        public bool LoadModel(string modelPath)
        {
            if (string.IsNullOrEmpty(modelPath))
                throw new ArgumentNullException(nameof(modelPath));

            if (!File.Exists(modelPath))
                throw new FileNotFoundException("The specified file does not exist.", modelPath);
            
            this.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(modelPath);
            var error = NativeMethods.net_Net_load_model_filepath(this.NativePtr, str, str.Length);
            if (error != NativeMethods.ErrorType.OK)
                return false;

            return true;
        }

        public bool LoadModel(DataReader dataReader)
        {
            if (dataReader == null) 
                throw new ArgumentNullException(nameof(dataReader));

            this.ThrowIfDisposed();
            dataReader.ThrowIfDisposed();

            var error = NativeMethods.net_Net_load_model_datareader(this.NativePtr, dataReader.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                return false;

            return true;
        }

        public bool LoadParam(string protoPath)
        {
            if (string.IsNullOrEmpty(protoPath))
                throw new ArgumentNullException(nameof(protoPath));

            if (!File.Exists(protoPath))
                throw new FileNotFoundException("The specified file does not exist.", protoPath);

            this.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(protoPath);
            var error = NativeMethods.net_Net_load_param_filepath(this.NativePtr, str, str.Length);
            if (error != NativeMethods.ErrorType.OK)
                return false;

            return true;
        }

        public bool LoadParam(DataReader dataReader)
        {
            if (dataReader == null) 
                throw new ArgumentNullException(nameof(dataReader));

            this.ThrowIfDisposed();
            dataReader.ThrowIfDisposed();

            var error = NativeMethods.net_Net_load_param_datareader(this.NativePtr, dataReader.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                return false;

            return true;
        }

        public bool RegisterCustomLayer(string type, DelegateHandler<LayerCreatorFunc> creator)
        {
            if (creator == null) 
                throw new ArgumentNullException(nameof(creator));

            this.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(type);
            var error = NativeMethods.net_Net_register_custom_layer(this.NativePtr, str, str.Length, creator.Handle);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return true;
        }

        public bool RegisterCustomLayer(int index, DelegateHandler<LayerCreatorFunc> creator)
        {
            if (creator == null)
                throw new ArgumentNullException(nameof(creator));

            this.ThrowIfDisposed();

            var error = NativeMethods.net_Net_register_custom_layer2(this.NativePtr, index, creator.Handle);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return true;
        }

        public void SetVulkanDevice(int deviceIndex)
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.net_Net_set_vulkan_device(this.NativePtr, deviceIndex);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void SetVulkanDevice(VulkanDevice device)
        {
            if (device == null) 
                throw new ArgumentNullException(nameof(device));

            this.ThrowIfDisposed();
            device.ThrowIfDisposed();

            var error = NativeMethods.net_Net_set_vulkan_device2(this.NativePtr, device.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
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

            NativeMethods.net_Net_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}