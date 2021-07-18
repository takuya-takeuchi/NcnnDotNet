using System.Threading.Tasks;

namespace YoloV3.Services
{

    public interface IFileAccessService
    {

        Task<byte[]> GetFileContent();

    }

}
