using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Squeeze
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Squeeze();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}