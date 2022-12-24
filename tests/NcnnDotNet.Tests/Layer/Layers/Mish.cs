using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Mish
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Mish();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}