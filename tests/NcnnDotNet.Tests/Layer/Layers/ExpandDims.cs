using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ExpandDims
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ExpandDims();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}