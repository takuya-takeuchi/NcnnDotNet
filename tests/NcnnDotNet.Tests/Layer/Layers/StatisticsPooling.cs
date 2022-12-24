using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Layers
{

    public class StatisticsPooling
    {

        [Fact]
        public void CreateAndDispose()
        {
            var layer = new NcnnDotNet.Layers.StatisticsPooling();
            layer.Dispose();
            Assert.True(layer.IsDisposed);
        }

    }

}