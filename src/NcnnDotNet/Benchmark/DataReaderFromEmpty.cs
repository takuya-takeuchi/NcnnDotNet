using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class DataReaderFromEmpty : DataReader
    {

        #region Constructors

        public DataReaderFromEmpty()
        {
            this.NativePtr = NativeMethods.datareader_DataReaderFromEmpty_new();
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

            NativeMethods.datareader_DataReaderFromEmpty_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}