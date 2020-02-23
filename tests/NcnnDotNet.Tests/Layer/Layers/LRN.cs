using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class LRN
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.LRN();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}