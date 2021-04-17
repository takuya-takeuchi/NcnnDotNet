using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class TanH
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.TanH();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}