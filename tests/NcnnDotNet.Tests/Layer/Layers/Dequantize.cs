using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Dequantize
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Dequantize();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}