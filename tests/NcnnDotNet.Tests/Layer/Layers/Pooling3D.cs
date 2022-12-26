using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Pooling3D
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Pooling3D();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}