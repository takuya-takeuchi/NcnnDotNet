using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Deconvolution1D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Deconvolution1D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}