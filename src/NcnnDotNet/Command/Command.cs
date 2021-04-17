using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public class Command : NcnnObject
    {

        #region Constructors

        public Command(VulkanDevice device, uint queueFamilyIndex)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            device.ThrowIfDisposed();

            NativeMethods.command_Command_new(device.NativePtr, queueFamilyIndex, out var ret);
            this.NativePtr = ret;
        }

        protected Command()
        {
        }

        #endregion

        #region Methods

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.command_Command_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}