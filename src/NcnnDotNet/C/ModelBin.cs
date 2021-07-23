using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.C
{

    public sealed class ModelBin : NcnnObject
    {

        #region Constructors

        internal ModelBin(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

    }

}