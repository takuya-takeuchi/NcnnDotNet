using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.C
{

    public sealed class CApi
    {

        #region Fields

        private const string TestDataDirectory = "TestData";

        #endregion

        #region Allocator

        [Fact]
        public void AllocatorCreatePoolAllocatorDestroy()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(allocator);

            NcnnDotNet.C.Ncnn.AllocatorDestroy(allocator);
        }

        [Fact]
        public void AllocatorCreateUnlockedPoolAllocatorDestroy()
        {
            var allocator = NcnnDotNet.C.Ncnn.AllocatorCreateUnlockedPoolAllocator();
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
        public void OptionCreateDestroy()
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

            if (Ncnn.IsSupportVulkan)
            {
                NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, false);
                Assert.True(NcnnDotNet.C.Ncnn.OptionGetUseVulkanCompute(option) == false);

                NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, true);
                Assert.True(NcnnDotNet.C.Ncnn.OptionGetUseVulkanCompute(option));
            }
            else
            {
                NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, false);
                Assert.True(NcnnDotNet.C.Ncnn.OptionGetUseVulkanCompute(option) == false);

                NcnnDotNet.C.Ncnn.OptionSetUseVulkanCompute(option, true);
                Assert.True(NcnnDotNet.C.Ncnn.OptionGetUseVulkanCompute(option) == false);
            }
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

        #region Mat Pixel

        //[Fact]
        //public void MatFromPixels()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var pixels = new byte[t.Stride * t.Height];
        //        var mat = NcnnDotNet.C.Ncnn.MatFromPixels(pixels, t.Type, t.Width, t.Height, t.Stride, allocator);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        [Fact]
        public void MatFromPixelsNullAllocator()
        {
            var targets = CreatePattern();
            foreach (var t in targets)
            {
                var pixels = new byte[t.Stride * t.Height];
                var mat = NcnnDotNet.C.Ncnn.MatFromPixels(pixels, t.Type, t.Width, t.Height, t.Stride);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatFromPixelsException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatFromPixels(null, PixelType.Bgr, 10, 10, 30);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatFromPixels)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        //[Fact]
        //public void MatFromPixelsResize()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var pixels = new byte[t.Stride * t.Height];
        //        var mat = NcnnDotNet.C.Ncnn.MatFromPixelsResize(pixels, t.Type, t.Width, t.Height, t.Stride, t.TargetWidth, t.TargetHeight, allocator);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        [Fact]
        public void MatFromPixelsResizeNullAllocator()
        {
            var targets = CreatePattern();
            foreach (var t in targets)
            {
                var pixels = new byte[t.Stride * t.Height];
                var mat = NcnnDotNet.C.Ncnn.MatFromPixelsResize(pixels, t.Type, t.Width, t.Height, t.Stride, t.TargetWidth, t.TargetHeight);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatFromPixelsResizeException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatFromPixelsResize(null, PixelType.Bgr, 10, 10, 30, 5, 5);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatFromPixelsResize)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        //[Fact]
        //public void MatFromPixelsRoi()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var pixels = new byte[t.Stride * t.Height];
        //        var mat = NcnnDotNet.C.Ncnn.MatFromPixelsRoi(pixels, t.Type, t.Width, t.Height, t.Stride, t.RoiX, t.RoiY, t.RoiWidth, t.RoiHeight, allocator);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        [Fact]
        public void MatFromPixelsRoiNullAllocator()
        {
            var targets = CreatePattern();
            foreach (var t in targets)
            {
                var pixels = new byte[t.Stride * t.Height];
                var mat = NcnnDotNet.C.Ncnn.MatFromPixelsRoi(pixels, t.Type, t.Width, t.Height, t.Stride, t.RoiX, t.RoiY, t.RoiWidth, t.RoiHeight);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatFromPixelsRoiException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatFromPixelsRoi(null, PixelType.Bgr, 10, 10, 30, 1, 1, 5, 5);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatFromPixelsRoi)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        //[Fact]
        //public void MatFromPixelsRoiResize()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var pixels = new byte[t.Stride * t.Height];
        //        var mat = NcnnDotNet.C.Ncnn.MatFromPixelsRoiResize(pixels, t.Type, t.Width, t.Height, t.Stride, t.RoiX, t.RoiY, t.RoiWidth, t.RoiHeight, t.TargetWidth, t.TargetHeight, allocator);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        [Fact]
        public void MatFromPixelsRoiResizeNullAllocator()
        {
            var targets = CreatePattern();
            foreach (var t in targets)
            {
                var pixels = new byte[t.Stride * t.Height];
                var mat = NcnnDotNet.C.Ncnn.MatFromPixelsRoiResize(pixels, t.Type, t.Width, t.Height, t.Stride, t.RoiX, t.RoiY, t.RoiWidth, t.RoiHeight, t.TargetWidth, t.TargetHeight);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatFromPixelsRoiResizeException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.MatFromPixelsRoiResize(null, PixelType.Bgr, 10, 10, 30, 1, 1, 5, 5, 5, 5);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatFromPixelsRoiResize)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        //[Fact]
        //public void MatToPixels()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel, allocator);
        //        var pixels = new byte[t.Stride * t.Height];
        //        NcnnDotNet.C.Ncnn.MatToPixels(mat, pixels, t.Type, t.Stride);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        [Fact]
        public void MatToPixelsNullAllocator()
        {
            var targets = CreatePattern3();
            foreach (var t in targets)
            {
                var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel);
                var pixels = new byte[t.Stride * t.Height];
                NcnnDotNet.C.Ncnn.MatToPixels(mat, pixels, t.Type, t.Stride);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatToPixelsException()
        {
            try
            {
                var pixels = new byte[300];
                NcnnDotNet.C.Ncnn.MatToPixels(null, pixels, PixelType.Bgr, 30);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatToPixels)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }

            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 10, 3);

            try
            {
                NcnnDotNet.C.Ncnn.MatToPixels(mat, null, PixelType.Bgr, 30);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatToPixels)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        //[Fact]
        //public void MatToPixelsResize()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern2();
        //    foreach (var t in targets)
        //    {
        //        var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel, allocator);
        //        var pixels = new byte[t.Stride * t.Height];
        //        NcnnDotNet.C.Ncnn.MatToPixelsResize(mat, pixels, t.Type, t.TargetWidth, t.TargetHeight, t.Stride);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        [Fact]
        public void MatToPixelsResizeNullAllocator()
        {
            var targets = CreatePattern2();
            foreach (var t in targets)
            {
                var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel);
                var pixels = new byte[t.TargetStride * t.TargetHeight];
                NcnnDotNet.C.Ncnn.MatToPixelsResize(mat, pixels, t.Type, t.TargetWidth, t.TargetHeight, t.Stride);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatToPixelsResizeException()
        {
            try
            {
                var pixels = new byte[15 * 5];
                NcnnDotNet.C.Ncnn.MatToPixelsResize(null, pixels, PixelType.Bgr, 5, 5, 15);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatToPixelsResize)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }

            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 10, 3);

            try
            {
                NcnnDotNet.C.Ncnn.MatToPixelsResize(mat, null, PixelType.Bgr, 5, 5, 15);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatToPixelsResize)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        //[Fact]
        //public void MatSubstractMeanNormalize()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel, allocator);
        //        var means = new float[t.Channel];
        //        var normalize = new float[t.Channel];
        //        NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, means, normalize);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        //[Fact]
        //public void MatSubstractMean()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel, allocator);
        //        var means = new float[t.Channel];
        //        NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, means, null);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        //[Fact]
        //public void MatSubstractNormalize()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    foreach (var t in targets)
        //    {
        //        var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel, allocator);
        //        var normalize = new float[t.Channel];
        //        NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, null, normalize);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //}

        [Fact]
        public void MatSubstractMeanNormalizeNullAllocator()
        {
            var targets = CreatePattern();
            foreach (var t in targets)
            {
                var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel);
                var means = new float[t.Channel];
                var normalize = new float[t.Channel];
                NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, means, normalize);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatSubstractMeanNullAllocator()
        {
            var targets = CreatePattern();
            foreach (var t in targets)
            {
                var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel);
                var means = new float[t.Channel];
                NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, means, null);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatSubstractNormalizeNullAllocator()
        {
            var targets = CreatePattern();
            foreach (var t in targets)
            {
                var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel);
                var normalize = new float[t.Channel];
                NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, null, normalize);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        [Fact]
        public void MatSubstractMeanNormalizeException()
        {
            var means = new float[3];
            var normalize = new float[3];
            var normalize2 = new float[2];

            try
            {
                NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(null, means, normalize);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }

            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 10, 3);

            try
            {
                NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, null, null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize)} should throw {nameof(ArgumentException)}");
            }
            catch (ArgumentException)
            {
                // Nothing to do
            }

            try
            {
                NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize(mat, means, normalize2);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.MatSubstractMeanNormalize)} should throw {nameof(ArgumentException)}");
            }
            catch (ArgumentException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        //[Fact]
        //public void ConvertPacking()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    var option = NcnnDotNet.C.Ncnn.OptionCreate();
        //    foreach (var t in targets)
        //    {
        //        var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel, allocator);
        //        NcnnDotNet.C.Ncnn.ConvertPacking(mat, out var ret, 1, option);
        //        NcnnDotNet.C.Ncnn.MatDestroy(ret);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //    NcnnDotNet.C.Ncnn.OptionDestroy(option);
        //}

        [Fact]
        public void ConvertPackingNullAllocator()
        {
            var targets = CreatePattern();
            var option = NcnnDotNet.C.Ncnn.OptionCreate();
            foreach (var t in targets)
            {
                var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel);
                NcnnDotNet.C.Ncnn.ConvertPacking(mat, out var ret, 1, option);
                NcnnDotNet.C.Ncnn.MatDestroy(ret);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
            NcnnDotNet.C.Ncnn.OptionDestroy(option);
        }

        [Fact]
        public void ConvertPackingException()
        {
            var option = NcnnDotNet.C.Ncnn.OptionCreate();

            try
            {
                NcnnDotNet.C.Ncnn.ConvertPacking(null, out _, 1, option);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ConvertPacking)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.OptionDestroy(option);
            }

            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 10, 3);

            try
            {
                NcnnDotNet.C.Ncnn.ConvertPacking(mat, out _, 1, null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ConvertPacking)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        //[Fact]
        //public void Flatten()
        //{
        //    var allocator = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
        //    Assert.NotNull(allocator);

        //    var targets = CreatePattern();
        //    var option = NcnnDotNet.C.Ncnn.OptionCreate();
        //    foreach (var t in targets)
        //    {
        //        var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel, allocator);
        //        NcnnDotNet.C.Ncnn.Flatten(mat, out var ret, option);
        //        NcnnDotNet.C.Ncnn.MatDestroy(ret);
        //        NcnnDotNet.C.Ncnn.MatDestroy(mat);
        //    }
        //    NcnnDotNet.C.Ncnn.OptionDestroy(option);
        //}

        [Fact]
        public void FlattenNullAllocator()
        {
            var targets = CreatePattern();
            var option = NcnnDotNet.C.Ncnn.OptionCreate();
            foreach (var t in targets)
            {
                var mat = NcnnDotNet.C.Ncnn.MatCreate3D(t.Width, t.Height, t.Channel);
                NcnnDotNet.C.Ncnn.Flatten(mat, out var ret, option);
                NcnnDotNet.C.Ncnn.MatDestroy(ret);
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
            NcnnDotNet.C.Ncnn.OptionDestroy(option);
        }

        [Fact]
        public void FlattenException()
        {
            var option = NcnnDotNet.C.Ncnn.OptionCreate();

            try
            {
                NcnnDotNet.C.Ncnn.Flatten(null, out _, option);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.Flatten)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.OptionDestroy(option);
            }

            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 10, 3);

            try
            {
                NcnnDotNet.C.Ncnn.Flatten(mat, out _, null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.Flatten)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }
        }

        #endregion

        #region Blob



        #endregion

        #region ParamDict

        [Fact]
        public void ParamDictCreateDestroy()
        {
            var paramDict = NcnnDotNet.C.Ncnn.ParamDictCreate();
            Assert.NotNull(paramDict);

            NcnnDotNet.C.Ncnn.ParamDictDestroy(paramDict);
        }

        [Fact]
        public void ParamDictDestroyException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ParamDictDestroy(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictDestroy)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void ParamDictGetType()
        {
            var paramDict = NcnnDotNet.C.Ncnn.ParamDictCreate();
            Assert.NotNull(paramDict);

            NcnnDotNet.C.Ncnn.ParamDictSetInt(paramDict, 0, 10);
            var type = NcnnDotNet.C.Ncnn.ParamDictGetType(paramDict, 0);
            Assert.Equal(ParamDictType.Int, type);

            NcnnDotNet.C.Ncnn.ParamDictSetFloat(paramDict, 1, 10);
            var type2 = NcnnDotNet.C.Ncnn.ParamDictGetType(paramDict, 1);
            Assert.Equal(ParamDictType.Float, type2);

            NcnnDotNet.C.Ncnn.ParamDictDestroy(paramDict);
        }

        [Fact]
        public void ParamDictGetTypeException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ParamDictGetType(null, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictGetType)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void ParamDictSetGetInt()
        {
            var paramDict = NcnnDotNet.C.Ncnn.ParamDictCreate();
            Assert.NotNull(paramDict);

            NcnnDotNet.C.Ncnn.ParamDictSetInt(paramDict, 0, 10);
            var value = NcnnDotNet.C.Ncnn.ParamDictGetInt(paramDict, 0, 0);
            Assert.Equal(10, value);

            NcnnDotNet.C.Ncnn.ParamDictDestroy(paramDict);
        }

        [Fact]
        public void ParamDictGetIntException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ParamDictGetInt(null, 0, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictGetInt)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void ParamDictSetGetFloat()
        {
            var paramDict = NcnnDotNet.C.Ncnn.ParamDictCreate();
            Assert.NotNull(paramDict);

            NcnnDotNet.C.Ncnn.ParamDictSetFloat(paramDict, 0, 10);
            var value = NcnnDotNet.C.Ncnn.ParamDictGetFloat(paramDict, 0, 0);
            Assert.Equal(10, value);

            NcnnDotNet.C.Ncnn.ParamDictDestroy(paramDict);
        }

        [Fact]
        public void ParamDictGetFloatException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ParamDictGetFloat(null, 0, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictGetFloat)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void ParamDictSetGetArray()
        {
            var paramDict = NcnnDotNet.C.Ncnn.ParamDictCreate();
            Assert.NotNull(paramDict);

            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 5, 3);
            var defaultValue = NcnnDotNet.C.Ncnn.MatCreate3D(1, 1, 1);

            NcnnDotNet.C.Ncnn.ParamDictSetArray(paramDict, 0, mat);
            var value = NcnnDotNet.C.Ncnn.ParamDictGetArray(paramDict, 0, defaultValue);
            Assert.Equal(10, NcnnDotNet.C.Ncnn.MatGetW(value));
            Assert.Equal(5, NcnnDotNet.C.Ncnn.MatGetH(value));
            Assert.Equal(3, NcnnDotNet.C.Ncnn.MatGetC(value));

            NcnnDotNet.C.Ncnn.MatDestroy(value);
            NcnnDotNet.C.Ncnn.MatDestroy(defaultValue);
            NcnnDotNet.C.Ncnn.MatDestroy(mat);
            NcnnDotNet.C.Ncnn.ParamDictDestroy(paramDict);
        }

        [Fact]
        public void ParamDictGetArrayException()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 10, 3);

            try
            {
                NcnnDotNet.C.Ncnn.ParamDictGetArray(null, 0, mat);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictGetArray)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }

            var paramDict = NcnnDotNet.C.Ncnn.ParamDictCreate();

            try
            {
                NcnnDotNet.C.Ncnn.ParamDictGetArray(paramDict, 0, null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictGetArray)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.ParamDictDestroy(paramDict);
            }
        }

        [Fact]
        public void ParamDictSetIntException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ParamDictSetInt(null, 0, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictSetInt)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void ParamDictSetFloatException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ParamDictSetFloat(null, 0, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictSetFloat)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void ParamDictSetArrayException()
        {
            var mat = NcnnDotNet.C.Ncnn.MatCreate3D(10, 10, 3);

            try
            {
                NcnnDotNet.C.Ncnn.ParamDictSetArray(null, 0, mat);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictSetArray)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.MatDestroy(mat);
            }

            var paramDict = NcnnDotNet.C.Ncnn.ParamDictCreate();

            try
            {
                NcnnDotNet.C.Ncnn.ParamDictSetArray(paramDict, 0, null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ParamDictSetArray)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
            finally
            {
                NcnnDotNet.C.Ncnn.ParamDictDestroy(paramDict);
            }
        }

        #endregion

        #region DataReader

        [Fact]
        public void DataReaderCreateDestroy()
        {
            var dataReader = NcnnDotNet.C.Ncnn.DataReaderCreate();
            Assert.NotNull(dataReader);

            NcnnDotNet.C.Ncnn.DataReaderDestroy(dataReader);
        }

        [Fact]
        public void DataReaderDestroyException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.DataReaderDestroy(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.DataReaderDestroy)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void DataReaderCreateFromMemory()
        {
            var dataReader = NcnnDotNet.C.Ncnn.DataReaderCreateFromMemory(new byte[100]);
            Assert.NotNull(dataReader);

            NcnnDotNet.C.Ncnn.DataReaderDestroy(dataReader);
        }

        #endregion

        #region ModelBin

        [Fact]
        public void ModelBinCreateFromDataReader()
        {
            var weights = Enumerable.Range(0, 10).Select(s => NcnnDotNet.C.Ncnn.MatCreate1D(10)).ToArray();

            var modelBin = NcnnDotNet.C.Ncnn.ModelBinCreateFromMatArray(weights, weights.Length);
            NcnnDotNet.C.Ncnn.ModelBinDestroy(modelBin);

            foreach (var weight in weights)
                NcnnDotNet.C.Ncnn.MatDestroy(weight);
        }

        [Fact]
        public void ModelBinCreateFromDataReaderException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ModelBinCreateFromDataReader(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ModelBinCreateFromDataReader)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        [Fact]
        public void ModelBinCreateFromMatArray()
        {
            var path = Path.Combine(TestDataDirectory, "mobilenet_ssd_voc_ncnn.bin");
            var memory = File.ReadAllBytes(path);

            var dataReader = NcnnDotNet.C.Ncnn.DataReaderCreateFromMemory(memory);
            var modelBin = NcnnDotNet.C.Ncnn.ModelBinCreateFromDataReader(dataReader);
            NcnnDotNet.C.Ncnn.ModelBinDestroy(modelBin);
            NcnnDotNet.C.Ncnn.DataReaderDestroy(dataReader);
        }

        [Fact]
        public void ModelBinCreateFromMatArrayException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ModelBinCreateFromMatArray(null, 0);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ModelBinCreateFromMatArray)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }

            var weights = Enumerable.Range(0, 10).Select(s => NcnnDotNet.C.Ncnn.MatCreate1D(10)).ToArray();

            try
            {
                NcnnDotNet.C.Ncnn.ModelBinCreateFromMatArray(weights, weights.Length + 1);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ModelBinCreateFromMatArray)} should throw {nameof(ArgumentException)}");
            }
            catch (ArgumentException)
            {
                // Nothing to do
            }
            finally
            {
                foreach (var weight in weights)
                    NcnnDotNet.C.Ncnn.MatDestroy(weight);
            }

            var weights2 = Enumerable.Range(0, 10).Select(s => NcnnDotNet.C.Ncnn.MatCreate1D(10)).ToArray();
            weights = weights2;

            try
            {
                weights[5] = null;
                NcnnDotNet.C.Ncnn.ModelBinCreateFromMatArray(weights, weights.Length);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ModelBinCreateFromMatArray)} should throw {nameof(ArgumentException)}");
            }
            catch (ArgumentException)
            {
                // Nothing to do
            }
            finally
            {
                foreach (var weight in weights2)
                    if (weight != null)
                        NcnnDotNet.C.Ncnn.MatDestroy(weight);
            }
        }

        [Fact]
        public void ModelBinDestroyException()
        {
            try
            {
                NcnnDotNet.C.Ncnn.ModelBinDestroy(null);
                Assert.False(true, $"{nameof(NcnnDotNet.C.Ncnn.ModelBinDestroy)} should throw {nameof(ArgumentNullException)}");
            }
            catch (ArgumentNullException)
            {
                // Nothing to do
            }
        }

        #endregion

        #region Helpers

        private static IEnumerable<PixelDataPattern> CreatePattern()
        {
            return new[]
            {
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Gray },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Gray },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba2Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra2Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba2Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra2Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba2Gray },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra2Gray },
            };
        }

        private static IEnumerable<PixelDataPattern> CreatePattern2()
        {
            return new[]
            {
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra2Rgba },
            };
        }

        private static IEnumerable<PixelDataPattern> CreatePattern3()
        {
            return new[]
            {
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Bgr },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Rgb },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Bgr2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 15, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 3, Stride = 30, Type = PixelType.Rgb2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Rgba },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 5,  RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 1, Stride = 10, Type = PixelType.Gray2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Rgba2Bgra },
                new PixelDataPattern { Width = 10, Height = 10, TargetWidth = 5, TargetHeight = 5, TargetStride = 20, RoiX = 1, RoiY = 1, RoiWidth = 5, RoiHeight = 5, Channel = 4, Stride = 40, Type = PixelType.Bgra2Rgba },
            };
        }

        #endregion

        private sealed class PixelDataPattern
        {

            public int Width { get; set; }
            public int Height { get; set; }
            public int Stride { get; set; }
            public int Channel { get; set; }
            public PixelType Type { get; set; }
            public int TargetWidth { get; set; }
            public int TargetHeight { get; set; }
            public int TargetStride { get; set; }
            public int RoiX { get; set; }
            public int RoiY { get; set; }
            public int RoiWidth { get; set; }
            public int RoiHeight { get; set; }

        }

    }

}