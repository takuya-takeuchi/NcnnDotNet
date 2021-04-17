using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Pooling
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Pooling();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}