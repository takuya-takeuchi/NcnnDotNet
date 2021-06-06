using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.C
{

    public sealed class Option : NcnnObject
    {

        #region Constructors

        internal Option(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

    }

}