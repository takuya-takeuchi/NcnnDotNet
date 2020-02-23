using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class MVN
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.MVN();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}