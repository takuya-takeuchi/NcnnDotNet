using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Requantize
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Requantize();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}