using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace NcnnDotNet.Extensions
{

    public static class DrawingExtensions
    {

        #region Fields

        private static readonly Color[] Palette;

        #endregion

        #region Constructors

        static DrawingExtensions()
        {
            Palette = new Color[256];
            for (var i = 0; i < 256; i++)
                Palette[i] = Color.FromArgb(i, i, i);
        }

        #endregion

        #region Methods

        public static Bitmap ToBitmap(this Mat mat, PixelType pixelType, PixelFormat pixelFormat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            mat.ThrowIfDisposed();

            var width = mat.W;
            var height = mat.H;

            switch (pixelType)
            {
                case PixelType.Rgb:
                    {
                        switch (pixelFormat)
                        {
                            case PixelFormat.Format24bppRgb:
                                return ToBitmap(mat.Data, 3, new Bitmap(width, height, pixelFormat), 3, true);
                            case PixelFormat.Format32bppArgb:
                                return ToBitmap(mat.Data, 3, new Bitmap(width, height, pixelFormat), 4, true);
                            case PixelFormat.Alpha:
                            case PixelFormat.Canonical:
                            case PixelFormat.DontCare:
                            case PixelFormat.Extended:
                            case PixelFormat.Format16bppArgb1555:
                            case PixelFormat.Format16bppGrayScale:
                            case PixelFormat.Format16bppRgb555:
                            case PixelFormat.Format16bppRgb565:
                            case PixelFormat.Format1bppIndexed:
                            case PixelFormat.Format32bppPArgb:
                            case PixelFormat.Format32bppRgb:
                            case PixelFormat.Format48bppRgb:
                            case PixelFormat.Format4bppIndexed:
                            case PixelFormat.Format64bppArgb:
                            case PixelFormat.Format64bppPArgb:
                            case PixelFormat.Format8bppIndexed:
                            case PixelFormat.Gdi:
                            case PixelFormat.Indexed:
                            case PixelFormat.Max:
                            case PixelFormat.PAlpha:
                            default:
                                throw new ArgumentOutOfRangeException(nameof(pixelFormat), pixelFormat, null);
                        }
                    }
                case PixelType.Bgr:
                    {
                        switch (pixelFormat)
                        {
                            case PixelFormat.Format24bppRgb:
                                return ToBitmap(mat.Data, 3, new Bitmap(width, height, pixelFormat), 3, false);
                            case PixelFormat.Format32bppArgb:
                                return ToBitmap(mat.Data, 3, new Bitmap(width, height, pixelFormat), 4, false);
                            case PixelFormat.Alpha:
                            case PixelFormat.Canonical:
                            case PixelFormat.DontCare:
                            case PixelFormat.Extended:
                            case PixelFormat.Format16bppArgb1555:
                            case PixelFormat.Format16bppGrayScale:
                            case PixelFormat.Format16bppRgb555:
                            case PixelFormat.Format16bppRgb565:
                            case PixelFormat.Format1bppIndexed:
                            case PixelFormat.Format32bppPArgb:
                            case PixelFormat.Format32bppRgb:
                            case PixelFormat.Format48bppRgb:
                            case PixelFormat.Format4bppIndexed:
                            case PixelFormat.Format64bppArgb:
                            case PixelFormat.Format64bppPArgb:
                            case PixelFormat.Format8bppIndexed:
                            case PixelFormat.Gdi:
                            case PixelFormat.Indexed:
                            case PixelFormat.Max:
                            case PixelFormat.PAlpha:
                            default:
                                throw new ArgumentOutOfRangeException(nameof(pixelFormat), pixelFormat, null);
                        }
                    }
                case PixelType.Gray:
                    {
                        switch (pixelFormat)
                        {
                            case PixelFormat.Format8bppIndexed:
                                return ToBitmap(mat.Data, 1, new Bitmap(width, height, pixelFormat), 1, false);
                            case PixelFormat.Alpha:
                            case PixelFormat.Canonical:
                            case PixelFormat.DontCare:
                            case PixelFormat.Extended:
                            case PixelFormat.Format16bppArgb1555:
                            case PixelFormat.Format16bppGrayScale:
                            case PixelFormat.Format16bppRgb555:
                            case PixelFormat.Format16bppRgb565:
                            case PixelFormat.Format1bppIndexed:
                            case PixelFormat.Format24bppRgb:
                            case PixelFormat.Format32bppArgb:
                            case PixelFormat.Format32bppPArgb:
                            case PixelFormat.Format32bppRgb:
                            case PixelFormat.Format48bppRgb:
                            case PixelFormat.Format4bppIndexed:
                            case PixelFormat.Format64bppArgb:
                            case PixelFormat.Format64bppPArgb:
                            case PixelFormat.Gdi:
                            case PixelFormat.Indexed:
                            case PixelFormat.Max:
                            case PixelFormat.PAlpha:
                            default:
                                throw new ArgumentOutOfRangeException(nameof(pixelFormat), pixelFormat, null);
                        }
                    }
                case PixelType.Rgba:
                    {
                        switch (pixelFormat)
                        {
                            case PixelFormat.Format32bppArgb:
                                return ToBitmap(mat.Data, 4, new Bitmap(width, height, pixelFormat), 4, true);
                            case PixelFormat.Alpha:
                            case PixelFormat.Canonical:
                            case PixelFormat.DontCare:
                            case PixelFormat.Extended:
                            case PixelFormat.Format16bppArgb1555:
                            case PixelFormat.Format16bppGrayScale:
                            case PixelFormat.Format16bppRgb555:
                            case PixelFormat.Format16bppRgb565:
                            case PixelFormat.Format1bppIndexed:
                            case PixelFormat.Format24bppRgb:
                            case PixelFormat.Format32bppPArgb:
                            case PixelFormat.Format32bppRgb:
                            case PixelFormat.Format48bppRgb:
                            case PixelFormat.Format4bppIndexed:
                            case PixelFormat.Format64bppArgb:
                            case PixelFormat.Format64bppPArgb:
                            case PixelFormat.Format8bppIndexed:
                            case PixelFormat.Gdi:
                            case PixelFormat.Indexed:
                            case PixelFormat.Max:
                            case PixelFormat.PAlpha:
                            default:
                                throw new ArgumentOutOfRangeException(nameof(pixelFormat), pixelFormat, null);
                        }
                    }
                case PixelType.Bgra:
                    {
                        switch (pixelFormat)
                        {
                            case PixelFormat.Format32bppArgb:
                                return ToBitmap(mat.Data, 4, new Bitmap(width, height, pixelFormat), 4, false);
                            case PixelFormat.Alpha:
                            case PixelFormat.Canonical:
                            case PixelFormat.DontCare:
                            case PixelFormat.Extended:
                            case PixelFormat.Format16bppArgb1555:
                            case PixelFormat.Format16bppGrayScale:
                            case PixelFormat.Format16bppRgb555:
                            case PixelFormat.Format16bppRgb565:
                            case PixelFormat.Format1bppIndexed:
                            case PixelFormat.Format24bppRgb:
                            case PixelFormat.Format32bppPArgb:
                            case PixelFormat.Format32bppRgb:
                            case PixelFormat.Format48bppRgb:
                            case PixelFormat.Format4bppIndexed:
                            case PixelFormat.Format64bppArgb:
                            case PixelFormat.Format64bppPArgb:
                            case PixelFormat.Format8bppIndexed:
                            case PixelFormat.Gdi:
                            case PixelFormat.Indexed:
                            case PixelFormat.Max:
                            case PixelFormat.PAlpha:
                            default:
                                throw new ArgumentOutOfRangeException(nameof(pixelFormat), pixelFormat, null);
                        }
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(pixelType));
            }
        }

        #region Helpers

        private static Bitmap ToBitmap(IntPtr source, int srcChannel, Bitmap dst, int dstChannel, bool swap)
        {
            BitmapData bitmapData = null;

            var format = dst.PixelFormat;

            try
            {
                var width = dst.Width;
                var height = dst.Height;
                bitmapData = dst.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, format);

                var srcStride = width * srcChannel;
                var dstStride = bitmapData.Stride;
                var hasPadding = dstStride != width * dstChannel;
                var destination = bitmapData.Scan0;

                if (swap)
                {
                    unsafe
                    {
                        if (srcChannel == dstChannel)
                        {
                            for (var y = 0; y < height; y++)
                            {
                                var pSrc = IntPtr.Add(source, srcStride * y);
                                var pDst = IntPtr.Add(destination, dstStride * y);

                                var pS = (byte*)pSrc;
                                var pD = (byte*)pDst;

                                for (var x = 0; x < width; x++)
                                {
                                    pD[0] = pS[2];
                                    pD[1] = pS[1];
                                    pD[2] = pS[0];
                                    if (srcChannel == 4)
                                        pD[3] = pS[3];

                                    pS += srcChannel;
                                    pD += dstChannel;
                                }
                            }
                        }
                        else
                        {
                            // set 255 as alpha channel
                            NativeMethods.cstd_memset(destination, 255, dstStride * height);

                            for (var y = 0; y < height; y++)
                            {
                                var pSrc = IntPtr.Add(source, srcStride * y);
                                var pDst = IntPtr.Add(destination, dstStride * y);

                                var pS = (byte*)pSrc;
                                var pD = (byte*)pDst;

                                for (var x = 0; x < width; x++)
                                {
                                    pD[0] = pS[2];
                                    pD[1] = pS[1];
                                    pD[2] = pS[0];

                                    pS += srcChannel;
                                    pD += dstChannel;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (srcChannel == dstChannel)
                    {
                        if (hasPadding)
                        {
                            for (var y = 0; y < height; y++)
                            {
                                var pSrc = IntPtr.Add(source, srcStride * y);
                                var pDst = IntPtr.Add(destination, dstStride * y);
                                NativeMethods.cstd_memcpy(pDst, pSrc, srcStride);
                            }
                        }
                        else
                            NativeMethods.cstd_memcpy(destination, source, dstStride * height);
                    }
                    else
                    {
                        // set 255 as alpha channel
                        NativeMethods.cstd_memset(destination, 255, dstStride * height);

                        unsafe
                        {
                            for (var y = 0; y < height; y++)
                            {
                                var pSrc = IntPtr.Add(source, srcStride * y);
                                var pDst = IntPtr.Add(destination, dstStride * y);

                                var pS = (byte*)pSrc;
                                var pD = (byte*)pDst;

                                for (var x = 0; x < width; x++)
                                {
                                    NativeMethods.cstd_memcpy(pD, pS, srcChannel);
                                    pS += srcChannel;
                                    pD += dstChannel;
                                }
                            }
                        }
                    }
                }

            }
            finally
            {
                if (bitmapData != null)
                    dst.UnlockBits(bitmapData);

                if (format == PixelFormat.Format8bppIndexed)
                {
                    var pal = dst.Palette;
                    for (var i = 0; i < 256; i++)
                        pal.Entries[i] = Palette[i];
                    dst.Palette = pal;
                }
            }

            return dst;
        }

        #endregion

        #endregion

    }

}