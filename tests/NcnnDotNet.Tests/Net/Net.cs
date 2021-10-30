using System.IO;
using Xunit;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Tests.Net
{

    public class Net
    {

        #region Fields

        private const string TestDataDirectory = "TestData";

        #endregion

        [Fact]
        public void LoadParamFromMemory()
        {
            var param = File.ReadAllBytes(Path.Combine(TestDataDirectory, "mobilenet_ssd_voc_ncnn.param"));
            using (var net = new NcnnDotNet.Net())
            using (var paramReader = new DataReaderFromMemory(param))
                Assert.True(net.LoadParam(paramReader), $"{nameof(NcnnDotNet.Net.LoadParam)} should return true");
        }

        [Fact]
        public void LoadModelFromMemory()
        {
            var param = File.ReadAllBytes(Path.Combine(TestDataDirectory, "mobilenet_ssd_voc_ncnn.param"));
            var bin = File.ReadAllBytes(Path.Combine(TestDataDirectory, "mobilenet_ssd_voc_ncnn.bin"));
            using (var net = new NcnnDotNet.Net())
            using (var paramReader = new DataReaderFromMemory(param))
            using (var binReader = new DataReaderFromMemory(bin))
            {
                // You shall call Net::load_param() first, then Net::load_model().
                Assert.True(net.LoadParam(paramReader), $"{nameof(NcnnDotNet.Net.LoadParam)} should return true");
                Assert.True(net.LoadModel(binReader), $"{nameof(NcnnDotNet.Net.LoadModel)} should return true");
            }
        }

    }

}