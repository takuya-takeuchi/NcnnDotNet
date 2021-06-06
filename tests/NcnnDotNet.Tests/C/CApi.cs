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
            var ret = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(ret);

            NcnnDotNet.C.Ncnn.AllocatorDestroy(ret);
        }

        [Fact]
        public void AllocatorCreateUnlockedPoolAllocator()
        {
            var ret = NcnnDotNet.C.Ncnn.AllocatorCreateUnlockedPoolAllocator();
            Assert.NotNull(ret);

            NcnnDotNet.C.Ncnn.AllocatorDestroy(ret);
        }

        [Fact]
        public void AllocatorDestroy()
        {
            var ret = NcnnDotNet.C.Ncnn.AllocatorCreatePoolAllocator();
            Assert.NotNull(ret);

            NcnnDotNet.C.Ncnn.AllocatorDestroy(ret);
        }

        #endregion

    }

}