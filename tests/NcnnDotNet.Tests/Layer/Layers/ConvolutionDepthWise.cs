using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ConvolutionDepthWise
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ConvolutionDepthWise();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}