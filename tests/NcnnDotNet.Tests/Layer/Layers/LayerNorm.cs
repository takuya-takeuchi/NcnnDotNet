using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class LayerNorm
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.LayerNorm();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}