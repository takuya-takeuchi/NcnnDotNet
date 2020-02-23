using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Quantize
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Quantize();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}