using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class GELU
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.GELU();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}