using System.Threading.Tasks;

namespace RenderImage.Portable.Services
{
    public interface IRenderService
    {
        Task<byte[]> RenderAsync();

        Task<byte[]> RenderAsync(int x, int y, int width, int height);
    }
}