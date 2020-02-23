using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Dropout
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Dropout();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}