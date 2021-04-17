using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class DetectionOutput
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.DetectionOutput();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}