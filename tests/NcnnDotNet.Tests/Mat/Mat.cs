using System;

using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Net
{

    public sealed class Mat : TestBase
    {

        [Fact]
        public void MatPixel0()
        {
            Assert.True(MatPixelGray(16, 16));
            Assert.True(MatPixelRgb(16, 16));
            Assert.True(MatPixelBgr(16, 16));
            Assert.True(MatPixelRgba(16, 16));
            Assert.True(MatPixelBgra(16, 16));
        }

        #region Helpers

        private static bool MatPixelGray(int w, int h)
        {
            var from = new [] { PixelType.Gray, PixelType.Gray2Rgb, PixelType.Gray2Bgr, PixelType.Gray2Rgba, PixelType.Gray2Bgra };
            var to = new [] { PixelType.Gray, PixelType.Rgb2Gray, PixelType.Bgr2Gray, PixelType.Rgba2Gray, PixelType.Bgra2Gray };

            using var a = RandomMat(w, h, 1);

            // FIXME enable more convert types
            for (var i = 0; i < 1; i++)
            {
                using var m = NcnnDotNet.Mat.FromPixels(a.Data, from[i], w, h);
                using var b = new NcnnDotNet.Mat(w, h, 1, (ulong)1);
                m.ToPixels(b.Data, to[i]);

                if (!MemCmp(a.Data, b.Data, w * h * 1))
                    return false;
            }

            return true;
        }

        private static bool MatPixelBgr(int w, int h)
        {
            var from = new [] { PixelType.Bgr, PixelType.Bgr2Rgb, PixelType.Bgr2Rgba, PixelType.Bgr2Bgra };
            var to = new [] { PixelType.Bgr, PixelType.Rgb2Bgr, PixelType.Rgba2Bgr, PixelType.Bgra2Bgr };

            using var a = RandomMat(w, h, 3);

            // FIXME enable more convert types
            for (var i = 0; i < 2; i++)
            {
                using var m = NcnnDotNet.Mat.FromPixels(a.Data, from[i], w, h);
                using var b = new NcnnDotNet.Mat(w, h, 3, (ulong)3);
                m.ToPixels(b.Data, to[i]);

                if (!MemCmp(a.Data, b.Data, w * h * 3))
                    return false;
            }

            return true;
        }

        private static bool MatPixelBgra(int w, int h)
        {
            var from = new [] { PixelType.Bgra, PixelType.Bgra2Rgba };
            var to = new [] { PixelType.Bgra, PixelType.Rgba2Bgra };

            using var a = RandomMat(w, h, 4);

            // FIXME enable more convert types
            for (var i = 0; i < 2; i++)
            {
                using var m = NcnnDotNet.Mat.FromPixels(a.Data, from[i], w, h);
                using var b = new NcnnDotNet.Mat(w, h, 4, (ulong)4);
                m.ToPixels(b.Data, to[i]);

                if (!MemCmp(a.Data, b.Data, w * h * 4))
                    return false;
            }

            return true;
        }

        private static bool MatPixelRgb(int w, int h)
        {
            var from = new [] { PixelType.Rgb, PixelType.Rgb2Bgr, PixelType.Rgb2Rgba, PixelType.Rgb2Bgra };
            var to = new [] { PixelType.Rgb, PixelType.Bgr2Rgb, PixelType.Rgba2Rgb, PixelType.Bgra2Rgb };

            using var a = RandomMat(w, h, 3);

            // FIXME enable more convert types
            for (var i = 0; i < 2; i++)
            {
                using var m = NcnnDotNet.Mat.FromPixels(a.Data, from[i], w, h);
                using var b = new NcnnDotNet.Mat(w, h, 3, (ulong)3);
                m.ToPixels(b.Data, to[i]);

                if (!MemCmp(a.Data, b.Data, w * h * 3))
                    return false;
            }

            return true;
        }

        private static bool MatPixelRgba(int w, int h)
        {
            var from = new [] { PixelType.Rgba, PixelType.Rgba2Bgra };
            var to = new [] { PixelType.Rgba, PixelType.Bgra2Rgba };

            using var a = RandomMat(w, h, 4);

            // FIXME enable more convert types
            for (var i = 0; i < 2; i++)
            {
                using var m = NcnnDotNet.Mat.FromPixels(a.Data, from[i], w, h);
                using var b = new NcnnDotNet.Mat(w, h, 4, (ulong)4);
                m.ToPixels(b.Data, to[i]);

                if (!MemCmp(a.Data, b.Data, w * h * 4))
                    return false;
            }

            return true;
        }

        private static bool MemCmp(IntPtr a, IntPtr b, int size)
        {
            unsafe
            {
                var pA = (byte*)a;
                var pB = (byte*)b;
                for (var i = 0; i < size; i++)
                    if (pA[i] != pB[i])
                        return false;
                return true;
            }
        }

        private static NcnnDotNet.Mat RandomMat(int w, int h, int elempack)
        {
            var m = new NcnnDotNet.Mat(w, h, elempack, (ulong)elempack);

            unsafe
            {
                var randam = new Random();
                var p = (byte*)m.Data;
                for (var i = 0; i < w * h * elempack; i++)
                    p[i] = (byte)(randam.Next() % 256);
            }

            return m;
        }

        #endregion

    }

}