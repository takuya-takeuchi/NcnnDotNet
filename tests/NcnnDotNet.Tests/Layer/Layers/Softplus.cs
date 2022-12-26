using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Softplus
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Softplus();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}