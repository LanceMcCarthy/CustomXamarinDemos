using System.Threading.Tasks;

namespace RenderImage.Portable.Services
{
    public interface IRenderService
    {
        /// <summary>
        /// Will render the entire display as an image byte[].
        /// </summary>
        /// <returns>Image byte array</returns>
        Task<byte[]> RenderAsync();

        /// <summary>
        /// Renders cropped area using absolute pixel position
        /// Ex. 50,50,300, 200
        /// </summary>
        /// <param name="x">Left</param>
        /// <param name="y">Top</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Image byte array</returns>
        Task<byte[]> RenderAsync(int x, int y, int width, int height);

        /// <summary>
        /// Renders cropped area using proportional percentage values.
        /// For example, if the X position is at pixel 100 on a 400 pixel wide display, X will be 25%. do the same for all 4 values.
        /// This proportional value allows the cropping to be scaled up to the device's specific resolution.
        /// </summary>
        /// <param name="xProportion">Left</param>
        /// <param name="yProportion">Top</param>
        /// <param name="widthProportion">Width</param>
        /// <param name="heightProportion">Height</param>
        /// <returns>Image byte array</returns>
        Task<byte[]> RenderRelativeAsync(int xProportion, int yProportion, int widthProportion, int heightProportion);
    }
}