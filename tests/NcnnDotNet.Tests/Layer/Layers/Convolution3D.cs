using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Convolution3D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Convolution3D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}