using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.C
{

    public sealed class Blob : NcnnObject
    {

        #region Constructors

        internal Blob(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

    }

}