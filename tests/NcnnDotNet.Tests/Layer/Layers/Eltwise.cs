using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Eltwise
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Eltwise();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}