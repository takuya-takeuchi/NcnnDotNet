using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Swish
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Swish();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}