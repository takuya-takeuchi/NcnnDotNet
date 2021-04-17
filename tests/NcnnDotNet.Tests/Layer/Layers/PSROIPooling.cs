using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class PSROIPooling
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.PSROIPooling();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}