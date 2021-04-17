using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class SPP
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.SPP();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}