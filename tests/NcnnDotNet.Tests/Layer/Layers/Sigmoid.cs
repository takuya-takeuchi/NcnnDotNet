using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Sigmoid
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Sigmoid();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}