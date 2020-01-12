using System;
using NcnnDotNet;

namespace BenchCNN
{

    internal sealed class DataReaderFromEmpty : DataReader
    {

        #region Methods

        #region Overrids

        protected override int OnScan(IntPtr format, IntPtr p)
        {
            return 0;
        }

        protected override long OnRead(IntPtr buf, long size)
        {
            return size;
        }

        #endregion

        #endregion

    }

}