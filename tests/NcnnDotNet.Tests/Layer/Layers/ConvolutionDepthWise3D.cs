using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ConvolutionDepthWise3D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ConvolutionDepthWise3D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}