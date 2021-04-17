using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ROIPooling
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ROIPooling();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}