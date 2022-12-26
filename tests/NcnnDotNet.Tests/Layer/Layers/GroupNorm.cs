using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class GroupNorm
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.GroupNorm();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}