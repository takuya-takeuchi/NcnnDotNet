using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class SELU
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.SELU();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}