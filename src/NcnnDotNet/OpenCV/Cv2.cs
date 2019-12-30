using System;
using System.IO;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    /// <summary>
    /// Provides the methods of OpenCV.
    /// </summary>
    public static partial class Cv2
    {

        #region Methods

        public static Mat ImRead(string fileName, CvLoadImage flags = CvLoadImage.Color)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException("The specified file does not exist.", fileName);

            var str = Ncnn.Encoding.GetBytes(fileName);
            var error = NativeMethods.opencv_imread(str, str.Length, (int) flags, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Mat(ret);
        }

        #endregion

    }

}