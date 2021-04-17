using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Normalize
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Normalize();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}