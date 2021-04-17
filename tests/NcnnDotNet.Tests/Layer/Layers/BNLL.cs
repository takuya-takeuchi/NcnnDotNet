using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class BNLL
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.BNLL();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}