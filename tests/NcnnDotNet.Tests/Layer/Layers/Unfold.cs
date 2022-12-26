using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Unfold
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Unfold();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}