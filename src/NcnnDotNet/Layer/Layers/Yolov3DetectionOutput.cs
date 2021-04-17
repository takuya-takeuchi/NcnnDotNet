using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Layers
{

    public sealed class Yolov3DetectionOutput : Layer
    {

        #region Constructors

        public Yolov3DetectionOutput()
        {
            NativeMethods.layer_layers_Yolov3DetectionOutput_new(out var ret);
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

            NativeMethods.layer_layers_Yolov3DetectionOutput_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}