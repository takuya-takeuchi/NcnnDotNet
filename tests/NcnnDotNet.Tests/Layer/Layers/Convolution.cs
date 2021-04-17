using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Convolution
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Convolution();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}