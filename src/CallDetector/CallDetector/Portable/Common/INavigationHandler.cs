using CallDetector.Portable.Models;

namespace CallDetector.Portable.Common
{
    public interface INavigationHandler
    {
        void LoadView(ViewType viewType);
    }
}