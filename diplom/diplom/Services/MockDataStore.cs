using diplom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Android.Content.Res;
using System.Diagnostics;

namespace diplom.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items = new List<Item>();

        public MockDataStore()
        {
            try
            {
                bool newTop;
                Root root1;
                var json = string.Empty;

                var context = Android.App.Application.Context;
                AssetManager assets = context.Assets;
                var assembly = typeof(App).GetTypeInfo().Assembly;
                using (var reader = new System.IO.StreamReader(assets.Open("file.json")))
                {
                    json = reader.ReadToEnd();
                    root1 = JsonConvert.DeserializeObject<Root>(json);
                }
                for (int i = 0; i < root1.topics.Count; i++)
                {
                    var item = new Item { Id = Guid.NewGuid().ToString(), Text = root1.topics[i].topic, Description = root1.topics[i].entries.Aggregate("", (acc, entrie) => acc + $"{entrie.word} - {entrie.translation}\n") };
                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public class Entry
        {
            public string word { get; set; }
            public string translation { get; set; }
        }

        public class Root
        {
            public List<Topic> topics { get; set; }
        }

        public class Topic
        {
            public string topic { get; set; }
            public List<Entry> entries { get; set; }
        }
        public static async void AddEntrie()
        {
           // bool newTop;
            Root root1;
            using (StreamReader r = new StreamReader("file.json"))
            {
                string json = File.ReadAllText("file.json");
                root1 = JsonConvert.DeserializeObject<Root>(json);
                //Console.WriteLine(JsonConvert.SerializeObject(root1, Formatting.Indented));
            }
            //string[] KeyValue = str.Split(" - ");
            //bool isTopicExists = false;
            //Entry entry = new Entry() { word = KeyValue[0], translation = KeyValue[1] };
            //string newJson = "";
            //foreach (Topic topic in root1.topics)
            //{
            //    if (topic.topic == thema)
            //    {
            //        topic.entries.Add(new Entry() { word = KeyValue[0], translation = KeyValue[1] });
            //        isTopicExists = true;
            //        break;
            //    }
            //}
            //if (!isTopicExists)
            //{
            //    Topic topic1;
            //    root1.topics.Add(topic1 = new Topic() { topic = thema, entries = new List<Entry>() { entry } });
            //}
            //string jsonString = JsonConvert.SerializeObject(root1, Formatting.Indented);
            //File.WriteAllText("file.json", jsonString);

        }
        public static string text;
        //public static void ReadTextfile()
        //{
        //    using (StreamReader sr = new StreamReader("thema12.txt"))
        //    {
        //        string line;
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            text = line;
        //            string thema = "Маўленчыя няправільнасці";
        //            AddEntrie(text, thema);
        //            Console.WriteLine(line);
        //        }
        //    }

        //}
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item, string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            Console.WriteLine(oldItem); //не понимаю почему не находит oldItem, он пустой почему то, хотя индекс находит
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);


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