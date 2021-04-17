using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ArgMax
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ArgMax();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}