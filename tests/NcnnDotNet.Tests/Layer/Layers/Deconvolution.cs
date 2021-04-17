using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Deconvolution
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Deconvolution();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}