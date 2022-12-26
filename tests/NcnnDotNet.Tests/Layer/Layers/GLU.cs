using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class GLU
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.GLU();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}