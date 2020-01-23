using System;

namespace RenderImage.Portable.Controls.LayoutService
{
    public class LayoutGuideChangedArgs : EventArgs
    {
        public LayoutGuideChangedArgs(LayoutGuide layoutGuide)
        {
            LayoutGuide = layoutGuide;
        }

        public LayoutGuide LayoutGuide { get; }
    }
}
