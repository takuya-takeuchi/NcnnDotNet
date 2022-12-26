using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class DeformableConv2D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.DeformableConv2D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}