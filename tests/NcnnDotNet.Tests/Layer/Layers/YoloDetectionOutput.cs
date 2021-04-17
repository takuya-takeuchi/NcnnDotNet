using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class YoloDetectionOutput
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.YoloDetectionOutput();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}