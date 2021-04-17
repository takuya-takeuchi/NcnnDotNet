using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class PReLU
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.PReLU();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}