using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class DeconvolutionDepthWise1D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.DeconvolutionDepthWise1D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}