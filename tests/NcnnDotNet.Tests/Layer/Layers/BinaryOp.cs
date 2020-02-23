using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class BinaryOp
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.BinaryOp();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}