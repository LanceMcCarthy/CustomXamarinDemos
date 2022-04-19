namespace ReverseLoadOnDemand.Portable
{
    public interface IScollableView
    {
        void OnItemVisualized(int newlyVisualizedItemId);
    }
}