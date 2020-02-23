using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Reorg
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Reorg();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}