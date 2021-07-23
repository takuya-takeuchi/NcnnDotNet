using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.C
{

    public sealed class ParamDict : NcnnObject
    {

        #region Constructors

        internal ParamDict(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

    }

}