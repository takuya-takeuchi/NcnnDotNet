using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class UnaryOp
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.UnaryOp();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}