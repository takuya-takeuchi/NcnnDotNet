using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Yolov3DetectionOutput
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Yolov3DetectionOutput();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}