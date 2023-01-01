using System.IO;

namespace NcnnDotNet.Extensions.Drawing.Tests
{

    public abstract class TestBase
    {

        #region Fields

        protected const string TestDataDirectory = "TestData";

        protected const string OutputDirectory = "OutputData";

        #endregion

        #region Methods

        protected FileInfo GetDataFile(string filename)
        {
            return new FileInfo(Path.Combine(TestDataDirectory, filename));
        }

        protected string GetOutDir(params string[] function)
        {
            var path = Path.Combine(OutputDirectory, Path.Combine(function));
            Directory.CreateDirectory(path);
            return path;
        }

        #endregion

    }

}
