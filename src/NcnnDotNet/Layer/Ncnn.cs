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

        public static Layer CreateLayer<T>(int index)
            where T : Layer, new()
        {
            var error = NativeMethods.layer_create_layer2(index, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            var layer = new T { NativePtr = ret };
            return layer;
        }

        public static int LayerToIndex(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            var str = Ncnn.Encoding.GetBytes(fileName);
            var error = NativeMethods.layer_layer_to_index(str, str.Length, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return ret;
        }

        #endregion

    }

}