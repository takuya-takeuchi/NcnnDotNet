using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class ShuffleChannel
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.ShuffleChannel();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}