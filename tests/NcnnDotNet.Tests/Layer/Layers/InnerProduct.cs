using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class InnerProduct
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.InnerProduct();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}