using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class GridSample
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.GridSample();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}