using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Reshape
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Reshape();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}