using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class MemoryData
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.MemoryData();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}