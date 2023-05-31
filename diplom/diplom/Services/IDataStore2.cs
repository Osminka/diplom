using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace diplom.Services
{
    public interface IDataStore2<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item, string id);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
