using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Flatten
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Flatten();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}