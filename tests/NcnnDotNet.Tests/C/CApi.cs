using System;
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
            Assert.True(NcnnDotNet.C.Ncnn.OptionGetUseVulkanCompute(option) == true);
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

    }

}