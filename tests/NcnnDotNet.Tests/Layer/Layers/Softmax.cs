using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Softmax
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Softmax();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}