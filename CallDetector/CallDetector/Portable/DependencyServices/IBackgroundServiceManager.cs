namespace CallDetector.Portable.DependencyServices
{
    public interface IBackgroundServiceManager
    {
        void StartService();
        void StopService();
    }
}