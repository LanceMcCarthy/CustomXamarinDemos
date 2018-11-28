using System;
using CallDetector.Portable.Common;

namespace CallDetector.Portable.DependencyServices
{
    public interface ICallManager
    {
        event EventHandler<CallStateChangedEventArgs> CallStatedChanged;

        void StartService();

        void StopService();
    }
}