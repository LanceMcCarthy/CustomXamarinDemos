using System.Collections.Generic;

namespace ReverseLoadOnDemand.Portable
{
    public interface IMyDataService
    {
        List<Item> GetItems(int offset, int count);
    }
}