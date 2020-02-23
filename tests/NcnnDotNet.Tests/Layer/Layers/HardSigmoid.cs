using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class HardSigmoid
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.HardSigmoid();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}