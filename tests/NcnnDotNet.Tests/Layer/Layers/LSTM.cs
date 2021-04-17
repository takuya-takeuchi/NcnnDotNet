using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class LSTM
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.LSTM();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}