using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ConvolutionDepthWise1D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ConvolutionDepthWise1D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}