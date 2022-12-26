using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Layers
{

    public sealed class Deconvolution3D : Layer
    {

        #region Constructors

        public Deconvolution3D()
        {
            NativeMethods.layer_layers_Deconvolution3D_new(out var ret);
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

            NativeMethods.layer_layers_Deconvolution3D_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}