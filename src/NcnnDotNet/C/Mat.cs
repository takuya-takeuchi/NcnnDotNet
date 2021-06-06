using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.C
{

    public sealed class Mat : NcnnObject
    {

        #region Constructors

        internal Mat(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

    }

}