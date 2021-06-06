using System;
using System.Runtime.InteropServices;
using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.C
{

    public class CApi
    {

        #region Allocator

        [Fact]
        public void AllocatorCreatePoolAllocator()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void AllocatorCreateUnlockedPoolAllocator()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreateUnlockedPoolAllocator();
            Assert.NotNull(allocator);

            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void AllocatorDestroy()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void AllocatorDestroyException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.AllocatorDestroy(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.AllocatorDestroy)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        #endregion

        #region Option

        [Fact]
        public void OptionCreate()
        {
            var ret = NcnnDotNet.C.Ncnn.OptionCreate();
            Assert.NotNull(ret);

            NcnnDotNet.C.Ncnn.OptionDestroy(ret);
        }

        [Fact]
        public void OptionDestroy()
        {
            var ret = NcnnDotNet.C.Ncnn.OptionCreate();
            Assert.NotNull(ret);

            NcnnDotNet.C.Ncnn.OptionDestroy(ret);
        }

        [Fact]
        public void OptionDestroyException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.OptionDestroy(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.OptionDestroy)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void OptionGetNumThreads()
        {
            var option = NcnnDotNet.C.Ncnn.OptionCreate();
            Assert.NotNull(option);

            var threads = NcnnDotNet.C.Ncnn.OptionGetNumThreads(option);
            Assert.True(threads > 0);

            NcnnDotNet.C.Ncnn.OptionDestroy(option);
        }

        [Fact]
        public void OptionGetNumThreadsException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.OptionGetNumThreads(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.OptionGetNumThreads)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void OptionSetNumThreads()
        {
            var option = NcnnDotNet.C.Ncnn.OptionCreate();
            Assert.NotNull(option);

            NcnnDotNet.C.Ncnn.OptionSetNumThreads(option, 1);
        }

        [Fact]
        public void OptionSetNumThreadsException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.OptionSetNumThreads(null, 1);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.OptionSetNumThreads)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void OptionGetUseVulkanCompute()
        {
            var option = NcnnDotNet.C.Ncnn.OptionCreate();
            Assert.NotNull(option);

            NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, false);
            Assert.True(NcnnDotNet.C.Ncnn.OptionGetUseVulkanCompute(option) == false);

            NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, true);
            Assert.True(NcnnDotNet.C.Ncnn.OptionGetUseVulkanCompute(option));
        }

        [Fact]
        public void OptionSetUseVulkanCompute()
        {
            var option = NcnnDotNet.C.Ncnn.OptionCreate();
            Assert.NotNull(option);

            NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, false);
            NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, true);
            NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, false);
            NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, true);
        }

        [Fact]
        public void OptionSetUseVulkanComputeException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(null, true);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        #endregion

        #region Mat

        //[Fact]
        //public void MatCreate1D()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100, allocator);

        //    NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        //}

        [Fact]
        public void MatCreate1DNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);

            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        //[Fact]
        //public void MatCreate2D()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var mat = NcnnDotNet.C.Ncnn.MatCreate2D(100, 100, allocator);

        //    NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        //}

        [Fact]
        public void MatCreate2DNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate2D(100, 100);

            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        //[Fact]
        //public void MatCreate3D()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var mat = NcnnDotNet.C.Ncnn.MatCreate3D(100, 100, 3, allocator);

        //    NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        //}

        [Fact]
        public void MatCreate3DNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(100, 100, 3);

            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatCreateExternal1D()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var data = Marshal.AllocCoTaskMem(100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal1D(100, data, allocator);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatCreateExternal1DNullAllocator()
        {
            var data = Marshal.AllocCoTaskMem(100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal1D(100, data);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatCreateExternal2D()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var data = Marshal.AllocCoTaskMem(100 * 100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal2D(100, 100, data, allocator);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatCreateExternal2DNullAllocator()
        {
            var data = Marshal.AllocCoTaskMem(100 * 100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal2D(100, 100, data);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatCreateExternal3D()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var data = Marshal.AllocCoTaskMem(100 * 100 * 3);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal3D(100, 100, 3, data, allocator);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatCreateExternal3DNullAllocator()
        {
            var data = Marshal.AllocCoTaskMem(100 * 100 * 3);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal3D(100, 100, 3, data);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        //[Fact]
        //public void MatCreate1DElem()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var mat = NcnnDotNet.C.Ncnn.MatCreate1DElem(100, 4, 1, allocator);

        //    NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        //}

        [Fact]
        public void MatCreate1DElemNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1DElem(100, 4, 1);

            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        //[Fact]
        //public void MatCreate2DElem()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var mat = NcnnDotNet.C.Ncnn.MatCreate2DElem(100, 100, 4, 1, allocator);

        //    NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        //}

        [Fact]
        public void MatCreate2DElemNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate2DElem(100, 100, 4, 1);

            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        //[Fact]
        //public void MatCreate3DElem()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var mat = NcnnDotNet.C.Ncnn.MatCreate3DElem(100, 100, 3, 4, 1, allocator);

        //    NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        //}

        [Fact]
        public void MatCreate3DElemNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate3DElem(100, 100, 3, 4, 1);

            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatCreateExternal1DElem()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var data = Marshal.AllocCoTaskMem(100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal1DElem(100 / 4, data, 4, 1, allocator);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatCreateExternal1DElemNullAllocator()
        {
            var data = Marshal.AllocCoTaskMem(100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal1DElem(100 / 4, data, 4, 1);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatCreateExternal2DElem()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var data = Marshal.AllocCoTaskMem(100 * 100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal2DElem(100 / 4, 100 / 4, data, 4, 1, allocator);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatCreateExternal2DElemNullAllocator()
        {
            var data = Marshal.AllocCoTaskMem(100 * 100);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal2DElem(100 / 4, 100 / 4, data, 4, 1);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatCreateExternal3DElem()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var data = Marshal.AllocCoTaskMem(100 * 100 * 3);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal3DElem(100 / 4, 100 / 4, 3, data, 4, 1, allocator);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatCreateExternal3DElemNullAllocator()
        {
            var data = Marshal.AllocCoTaskMem(100 * 100 * 3);
            var mat = NcnnDotNet.C.Ncnn.MatCreateExternal3DElem(100 / 4, 100 / 4, 3, data, 4, 1);

            Marshal.FreeCoTaskMem(data);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatDestroy()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatDestroyException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatDestroy(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatDestroy)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatFillFloat()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            NcnnDotNet.C.Ncnn.MatFillFloat(mat, 0);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatFillFloatException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatFillFloat(null, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatFillFloat)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        //[Fact]
        //public void MatClone()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100, allocator);
        //    var mat2 = NcnnDotNet.C.Ncnn.MatClone(mat, allocator);

        //    NcnnDotNet.C.Ncnn.MatDestroy(mat2);
        //    NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        //}

        [Fact]
        public void MatCloneNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            var mat2 = NcnnDotNet.C.Ncnn.MatClone(mat);

            NcnnDotNet.C.Ncnn.MatDestroy(mat2);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatCloneException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatClone(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatClone)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatReshape1D()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            var mat2 = NcnnDotNet.C.Ncnn.MatReshape1D(mat, 10, allocator);

            NcnnDotNet.C.Ncnn.MatDestroy(mat2);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatReshape1DNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            var mat2 = NcnnDotNet.C.Ncnn.MatReshape1D(mat, 10);

            NcnnDotNet.C.Ncnn.MatDestroy(mat2);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatReshape1DException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatReshape1D(null, 100);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatReshape1D)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatReshape2D()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var mat = NcnnDotNet.C.Ncnn.MatCreate2D(100, 100);
            var mat2 = NcnnDotNet.C.Ncnn.MatReshape2D(mat, 10, 10, allocator);

            NcnnDotNet.C.Ncnn.MatDestroy(mat2);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatReshape2DNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate2D(100, 100);
            var mat2 = NcnnDotNet.C.Ncnn.MatReshape2D(mat, 10, 10);

            NcnnDotNet.C.Ncnn.MatDestroy(mat2);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatReshape2DException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatReshape2D(null, 100, 100);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatReshape2D)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatReshape3D()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(100, 100, 3);
            var mat2 = NcnnDotNet.C.Ncnn.MatReshape3D(mat, 10, 10, 3, allocator);

            NcnnDotNet.C.Ncnn.MatDestroy(mat2);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void MatReshape3DNullAllocator()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(100, 100, 3);
            var mat2 = NcnnDotNet.C.Ncnn.MatReshape3D(mat, 10, 10, 3);

            NcnnDotNet.C.Ncnn.MatDestroy(mat2);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
        }

        [Fact]
        public void MatReshape3DException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatReshape3D(null, 100, 100, 3);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatReshape3D)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetDims()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.Equal(1, NcnnDotNet.C.Ncnn.MatGetDims(mat));
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(100, 100);
            Assert.Equal(2, NcnnDotNet.C.Ncnn.MatGetDims(mat2));
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(100, 100, 3);
            Assert.Equal(3, NcnnDotNet.C.Ncnn.MatGetDims(mat3));
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetDimsException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetDims(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetDims)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetW()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.Equal(100, NcnnDotNet.C.Ncnn.MatGetW(mat));
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.Equal(200, NcnnDotNet.C.Ncnn.MatGetW(mat2));
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 100, 3);
            Assert.Equal(300, NcnnDotNet.C.Ncnn.MatGetW(mat3));
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetWException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetW(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetW)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetH()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.Equal(1, NcnnDotNet.C.Ncnn.MatGetH(mat));
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.Equal(100, NcnnDotNet.C.Ncnn.MatGetH(mat2));
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 150, 3);
            Assert.Equal(150, NcnnDotNet.C.Ncnn.MatGetH(mat3));
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetHException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetH(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetH)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetC()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.Equal(1, NcnnDotNet.C.Ncnn.MatGetC(mat));
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.Equal(1, NcnnDotNet.C.Ncnn.MatGetC(mat2));
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 150, 3);
            Assert.Equal(3, NcnnDotNet.C.Ncnn.MatGetC(mat3));
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetCException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetC(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetC)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetElemSize()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.Equal(4u, NcnnDotNet.C.Ncnn.MatGetElemSize(mat));
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.Equal(4u, NcnnDotNet.C.Ncnn.MatGetElemSize(mat2));
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 150, 3);
            Assert.Equal(4u, NcnnDotNet.C.Ncnn.MatGetElemSize(mat3));
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetElemSizeException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetElemSize(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetElemSize)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetElemPack()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.Equal(1, NcnnDotNet.C.Ncnn.MatGetElemPack(mat));
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.Equal(1, NcnnDotNet.C.Ncnn.MatGetElemPack(mat2));
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 150, 3);
            Assert.Equal(1, NcnnDotNet.C.Ncnn.MatGetElemPack(mat3));
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetElemPackException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetElemPack(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetElemPack)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetCStep()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.Equal(100u, NcnnDotNet.C.Ncnn.MatGetCStep(mat));
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.Equal((ulong)(200 * 100), NcnnDotNet.C.Ncnn.MatGetCStep(mat2));
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 150, 3);
            Assert.Equal((ulong)(300 * 150), NcnnDotNet.C.Ncnn.MatGetCStep(mat3));
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetCStepException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetCStep(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetCStep)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetData()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.True(NcnnDotNet.C.Ncnn.MatGetData(mat) != IntPtr.Zero);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.True(NcnnDotNet.C.Ncnn.MatGetData(mat2) != IntPtr.Zero);
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 150, 3);
            Assert.True(NcnnDotNet.C.Ncnn.MatGetData(mat3) != IntPtr.Zero);
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetDataException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetData(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetCStep)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void MatGetChannelData()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate1D(100);
            Assert.True(NcnnDotNet.C.Ncnn.MatGetChannelData(mat, 0) != IntPtr.Zero);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);

            var mat2 = NcnnDotNet.C.Ncnn.MatCreate2D(200, 100);
            Assert.True(NcnnDotNet.C.Ncnn.MatGetChannelData(mat2, 0) != IntPtr.Zero);
            NcnnDotNet.C.Ncnn.MatDestroy(mat2);

            var mat3 = NcnnDotNet.C.Ncnn.MatCreate3D(300, 150, 3);
            Assert.True(NcnnDotNet.C.Ncnn.MatGetChannelData(mat3, 0) != IntPtr.Zero);
            NcnnDotNet.C.Ncnn.MatDestroy(mat3);
        }

        [Fact]
        public void MatGetChannelDataException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatGetChannelData(null, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatGetChannelData)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        #endregion

    }

}