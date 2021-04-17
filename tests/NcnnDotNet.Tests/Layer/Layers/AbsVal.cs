using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class AbsVal
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.AbsVal();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}