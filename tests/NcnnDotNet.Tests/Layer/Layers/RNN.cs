using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class RNN
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.RNN();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}