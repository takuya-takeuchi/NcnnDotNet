using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class MatMul
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.MatMul();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}