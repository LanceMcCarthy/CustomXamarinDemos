using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace RenderImage.Portable.Controls.HingeService
{
    public interface IHingeService : INotifyPropertyChanged, IDisposable
    {
        event EventHandler<HingeEventArgs> OnHingeUpdated;

        bool IsSpanned { get; }

        bool IsLandscape { get; }

        Rectangle GetHinge();
    }
}
