using System;

namespace NcnnDotNet
{

    /// <summary>
    /// The exception is general exception for ncnn. This class cannot be inherited.
    /// </summary>
    public sealed class NcnnException : Exception
    {

        #region Constructors

        internal NcnnException(string message) :
            base(message)
        {
        }

        #endregion

    }

}
