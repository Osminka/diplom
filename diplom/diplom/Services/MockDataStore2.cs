using diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diplom.Services
{
    public class MockDataStore2 : IDataStore<Item2>
    {
        readonly List<Item2> items;

        public MockDataStore2()
        {
            items = new List<Item2>()
            {
                new Item2 { Id = Guid.NewGuid().ToString(), Source = "https://resheba.top/_pu/0/25728958.jpg",},
                new Item2 { Id = Guid.NewGuid().ToString(), Source = "https://resheba.top/_pu/2/99082271.png",},
                new Item2 { Id = Guid.NewGuid().ToString(), Source = "https://resheba.top/_pu/0/25728958.jpg" },
                new Item2 { Id = Guid.NewGuid().ToString(), Source = "https://resheba.top/_pu/0/25728958.jpg" },
                new Item2 { Id = Guid.NewGuid().ToString(), Source = "https://resheba.top/_pu/0/25728958.jpg" },
                new Item2 { Id = Guid.NewGuid().ToString(), Source = "https://resheba.top/_pu/0/25728958.jpg" },
                new Item2 { Id = Guid.NewGuid().ToString(), Source = "https://resheba.top/_pu/0/25728958.jpg" },
     

            };
        }

        public async Task<bool> AddItemAsync(Item2 item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item2 item, string id)
        {
            var oldItem = items.Where((Item2 arg) => arg.Id == id).FirstOrDefault();
            Console.WriteLine(oldItem); //не понимаю почему не находит oldItem, он пустой почему то, хотя индекс находит
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item2 arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);


            return await Task.FromResult(true);
        }

        public async Task<Item2> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item2>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}