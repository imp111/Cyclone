using Cyclone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyclone.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        private readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Paris", Unit = "metric"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Madrid", Unit = "metric" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "London", Unit = "metric"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Rome", Unit = "metric"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "New York", Unit = "metric"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Tokyo", Unit = "metric"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Moscow", Unit = "metric"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Beijing", Unit = "metric"},
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string text)
        {
            var enteredItem = items.Where((Item arg) => arg.Text.ToLower() == text.ToLower()).FirstOrDefault();
            items.Remove(enteredItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}