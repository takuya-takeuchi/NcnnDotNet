using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class DeconvolutionDepthWise
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.DeconvolutionDepthWise();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}