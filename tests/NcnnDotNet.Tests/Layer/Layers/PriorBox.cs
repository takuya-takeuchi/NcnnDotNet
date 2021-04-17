using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class PriorBox
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.PriorBox();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}