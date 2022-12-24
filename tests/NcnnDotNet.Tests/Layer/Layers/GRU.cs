using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class GRU
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.GRU();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}