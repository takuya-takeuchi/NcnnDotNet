using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Layers
{

    public sealed class PSROIPooling : Layer
    {

        #region Constructors

        public PSROIPooling()
        {
            NativeMethods.layer_layers_PSROIPooling_new(out var ret);
            this.NativePtr = ret;
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            // Suspend to call Layer.DisposeUnmanaged
            //base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.layer_layers_PSROIPooling_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}