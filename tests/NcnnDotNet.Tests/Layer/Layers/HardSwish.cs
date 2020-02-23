using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class HardSwish
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.HardSwish();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}