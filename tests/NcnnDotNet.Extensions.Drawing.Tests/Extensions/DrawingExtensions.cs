using System.Drawing.Imaging;
using System.IO;
using Xunit;
using NcnnDotNet.OpenCV;
using NcnnDotNet.Extensions;

namespace NcnnDotNet.Extensions.Drawing.Tests.Extensions
{

    public sealed class DrawingExtensions : TestBase
    {

        [Fact]
        public void ToBitmap()
        {
            var testCases = new[]
            {
                new { FileName = "LennaBgr.png",         LoadColor = CvLoadImage.Unchanged, PixelType = PixelType.Bgr,  ElemSize = 3, PixelFormat = PixelFormat.Format24bppRgb },
                new { FileName = "LennaBgrPadding.png",  LoadColor = CvLoadImage.Unchanged, PixelType = PixelType.Bgr,  ElemSize = 3, PixelFormat = PixelFormat.Format24bppRgb },
                new { FileName = "LennaBgra.png",        LoadColor = CvLoadImage.Unchanged, PixelType = PixelType.Bgra, ElemSize = 4, PixelFormat = PixelFormat.Format32bppArgb },
                new { FileName = "LennaBgraPadding.png", LoadColor = CvLoadImage.Unchanged, PixelType = PixelType.Bgra, ElemSize = 4, PixelFormat = PixelFormat.Format32bppArgb },
                new { FileName = "LennaGray.png",        LoadColor = CvLoadImage.Grayscale, PixelType = PixelType.Gray, ElemSize = 1, PixelFormat = PixelFormat.Format8bppIndexed },
                new { FileName = "LennaGrayPadding.png", LoadColor = CvLoadImage.Grayscale, PixelType = PixelType.Gray, ElemSize = 1, PixelFormat = PixelFormat.Format8bppIndexed }
            };

            foreach (var testCase in testCases)
            {
                var file = this.GetDataFile(testCase.FileName);

                using var mat = Cv2.ImRead(file.FullName, testCase.LoadColor);
                using var m1 = Mat.FromPixels(mat.Data, testCase.PixelType, mat.Cols, mat.Rows);
                using var m2 = new Mat(mat.Cols, mat.Rows, testCase.ElemSize);
                m1.ToPixels(m2.Data, testCase.PixelType);
                using var bitmap = m2.ToBitmap(testCase.PixelType, testCase.PixelFormat);

                Assert.Equal(m2.W, bitmap.Width);
                Assert.Equal(m2.H, bitmap.Height);
                Assert.Equal(testCase.PixelFormat, bitmap.PixelFormat);

                var outDir = this.GetOutDir(nameof(this.ToBitmap));
                var outFile = Path.Combine(outDir, testCase.FileName);

                bitmap.Save(outFile);
            }
        }

    }

}