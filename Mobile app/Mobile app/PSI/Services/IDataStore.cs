using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSI.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T Route);
        Task<bool> UpdateItemAsync(T Route);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
