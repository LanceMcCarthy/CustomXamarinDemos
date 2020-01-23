using Xamarin.Forms;

namespace RenderImage.Portable.Controls.LayoutService
{
    public class LayoutGuide
    {
        public LayoutGuide(string name, Rectangle rectangle)
        {
            Name = name;
            Rectangle = rectangle;
        }

        public string Name { get; }
        public Rectangle Rectangle { get; }
    }
}
