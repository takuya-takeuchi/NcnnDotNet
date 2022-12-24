using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class DeepCopy
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.DeepCopy();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}