using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RenderImage.Portable.Controls.LayoutService
{
    public interface ILayoutService
    {
        Point? GetLocationOnScreen(VisualElement visualElement);

        void AddLayoutGuide(string name, Rectangle location);

        IReadOnlyDictionary<string, LayoutGuide> LayoutGuides { get; }

        event EventHandler<LayoutGuideChangedArgs> LayoutGuideChanged;
    }
}
