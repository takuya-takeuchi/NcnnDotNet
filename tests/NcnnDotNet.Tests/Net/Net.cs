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
            var path = Path.Combine(TestDataDirectory, "mobilenet_ssd_voc_ncnn.param");
            var memory = File.ReadAllBytes(path);
            using (var net = new NcnnDotNet.Net())
            using (var reader = new DataReaderFromMemory(memory))
                Assert.True(net.LoadParam(reader), $"{nameof(NcnnDotNet.Net.LoadParam)} should return true");
        }

        [Fact]
        public void LoadModelFromMemory()
        {
            var path = Path.Combine(TestDataDirectory, "mobilenet_ssd_voc_ncnn.bin");
            var memory = File.ReadAllBytes(path);
            using (var net = new NcnnDotNet.Net())
            using (var reader = new DataReaderFromMemory(memory))
                Assert.True(net.LoadModel(reader), $"{nameof(NcnnDotNet.Net.LoadModel)} should return true");
        }

    }

}