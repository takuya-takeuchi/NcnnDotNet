using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Einsum
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Einsum();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}