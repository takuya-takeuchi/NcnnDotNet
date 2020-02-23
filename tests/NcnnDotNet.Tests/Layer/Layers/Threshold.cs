using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Threshold
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Threshold();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}