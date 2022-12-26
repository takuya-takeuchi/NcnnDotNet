using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Gemm
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Gemm();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}