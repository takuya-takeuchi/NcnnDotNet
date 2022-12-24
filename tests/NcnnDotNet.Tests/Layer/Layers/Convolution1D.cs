using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Convolution1D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Convolution1D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}