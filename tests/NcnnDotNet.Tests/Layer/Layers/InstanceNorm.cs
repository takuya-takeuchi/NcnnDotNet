using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class InstanceNorm
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.InstanceNorm();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}