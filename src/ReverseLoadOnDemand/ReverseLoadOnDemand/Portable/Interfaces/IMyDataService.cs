using System.Collections.Generic;
using ReverseLoadOnDemand.Portable.Models;

namespace ReverseLoadOnDemand.Portable.Interfaces;

public interface IMyDataService
{
    List<Item> GetItems(int offset, int count);
}