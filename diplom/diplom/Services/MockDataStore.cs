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
using System.Linq.Expressions;
using MySqlConnector;

namespace diplom.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items = new List<Item>();

        public MockDataStore()
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();
                var command = new MySqlCommand(@"SELECT * FROM Words", conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Чтение данных из текущей строки результата
                        string topic = reader.GetString("Topic");
                        string word = reader.GetString("Text");

                        // Обработка полученных данных
                        //Console.WriteLine($"topic: {topic}, word: {word}");
                        var item = new Item { Id = Guid.NewGuid().ToString(), Text = topic, Description = word };
                        items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
            }
        }

        static MySqlDataReader Count;
        static string Ok = "";
        public static async void AddEntrie(string topic, string text)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();              
                var command = new MySqlCommand(@"SELECT * FROM Words WHERE Topic=N'" + topic +"'", conn);

                var count = command.ExecuteReader();
                Count = count;
                if (count.HasRows)
                {
                    App.Current.MainPage.DisplayAlert("Внимание!", "Такая тема уже есть", "Ok");
                }
                else
                {
                    count.Close();
                    Ok = "ok";
                    command = new MySqlCommand(@"INSERT INTO Words (Topic, Text) VALUES (N'" + topic + "', N'" + text + "')", conn);
                    command.ExecuteNonQuery();

                    conn.Close();
                    App.Current.MainPage.DisplayAlert("Внимание!", "Запись добавлена", "Ok");
                }

            
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void UpdateEntrie(string oldTopic, string topic, string text)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();

                var command = new MySqlCommand(@"UPDATE Words SET Text = @text, Topic = @newTopic WHERE Topic = @oldTopic", conn);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@newTopic", topic);
                command.Parameters.AddWithValue("@oldTopic", oldTopic);
                command.ExecuteNonQuery();

                conn.Close();
                Ok = "ok";
                App.Current.MainPage.DisplayAlert("Внимание!", "Запись обновлена", "Ok");


            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static async void DeleteEntrie(string topic)
        {
            try
            {
                var conn = new MySqlConnection(Properties.Resources.db_mobile);
                conn.Open();

                var command = new MySqlCommand(@"DELETE FROM Words WHERE Topic = @topic", conn);
                command.Parameters.AddWithValue("@topic", topic);
                command.ExecuteNonQuery();

                conn.Close();
                Ok = "ok";
                App.Current.MainPage.DisplayAlert("Внимание!", "Запись удалена", "Ok");
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Внимание!", "Произошла ошибка, проверьте ваше подключение к интернету", "Ok");
                Console.Out.WriteLineAsync("SQL_ERROR: " + ex.ToString() + "\n" + ex.StackTrace);
                //await App.Current.MainPage.DisplayAlert("Внимание!", "Проверьте ваше подключение к интернету!", "Ok");
            }

        }
        public static string text;
        public async Task<bool> AddItemAsync(Item item)
        {
            AddEntrie(item.Text, item.Description);
            if (Ok == "ok")
            {
                items.Add(item);
                Ok = "";
            }
                
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item, string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            UpdateEntrie(oldItem.Text, item.Text, item.Description);
            if (Ok == "ok")
            {
                items.Remove(oldItem);
                items.Add(item);
                Ok = "";
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            DeleteEntrie(oldItem.Text);
           
            if (Ok == "ok")
            {
                items.Remove(oldItem);
                Ok = "";
            }
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