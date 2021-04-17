using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ROIAlign
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ROIAlign();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}