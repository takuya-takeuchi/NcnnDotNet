using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    /// <summary>
    /// Provides the methods of ncnn.
    /// </summary>
    public static partial class Ncnn
    {

        #region Methods

        public static Layer CreateLayer(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            var str = Ncnn.Encoding.GetBytes(fileName);
            var error = NativeMethods.layer_create_layer(str, str.Length, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Layer(ret);
        }
        
        #endregion

    }

}