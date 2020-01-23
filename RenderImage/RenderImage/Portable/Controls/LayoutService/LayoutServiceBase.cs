using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RenderImage.Portable.Controls.LayoutService
{
    public abstract class LayoutServiceBase : ILayoutService
    {
        protected LayoutServiceBase()
        {
            LayoutGuides = LayoutGuidesInternal;
        }

        public event EventHandler<LayoutGuideChangedArgs> LayoutGuideChanged;

        public IReadOnlyDictionary<string, LayoutGuide> LayoutGuides { get; }

        Dictionary<string, LayoutGuide> LayoutGuidesInternal { get; } = new Dictionary<string, LayoutGuide>();

        public void AddLayoutGuide(string name, Rectangle location)
        {
            var guide = new LayoutGuide(name, location);
            LayoutGuidesInternal[name] = guide;
            LayoutGuideChanged?.Invoke(this, new LayoutGuideChangedArgs(guide));
        }

        public abstract Point? GetLocationOnScreen(VisualElement visualElement);
    }
}
