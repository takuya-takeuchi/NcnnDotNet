using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class DataReaderFromMemory : DataReader
    {

        #region Constructors

        public DataReaderFromMemory(byte[] memory)
        {
            if (memory == null)
                throw new ArgumentNullException(nameof(memory));

            this.NativePtr = NativeMethods.datareader_DataReaderFromMemory_new(memory, (uint)memory.Length);
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

            NativeMethods.datareader_DataReaderFromMemory_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}