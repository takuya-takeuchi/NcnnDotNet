using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class DeconvolutionDepthWise3D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.DeconvolutionDepthWise3D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}