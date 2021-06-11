using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.C
{

    public sealed class DataReader : NcnnObject
    {

        #region Constructors

        internal DataReader(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

    }

}