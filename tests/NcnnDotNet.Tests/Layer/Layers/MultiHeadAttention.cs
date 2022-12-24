using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class MultiHeadAttention
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.MultiHeadAttention();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}