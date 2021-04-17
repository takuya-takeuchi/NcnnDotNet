using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Reduction
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Reduction();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}