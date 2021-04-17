using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class PixelShuffle
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.PixelShuffle();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}