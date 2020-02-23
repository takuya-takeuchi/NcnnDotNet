using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class Proposal
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.Proposal();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}