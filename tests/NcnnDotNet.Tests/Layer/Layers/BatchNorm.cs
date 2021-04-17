using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class BatchNorm
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.BatchNorm();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}